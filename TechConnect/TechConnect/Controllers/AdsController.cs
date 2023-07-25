using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TechConnect.Core;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;
using Microsoft.AspNetCore.Hosting;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using Amazon.Runtime;

namespace TechConnect.Controllers
{
    public class AdsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IService<SpecialVehicleModel, int> _specialVehicleService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "techconnect1404";

        public AdsController(IService<SpecialVehicleModel, int> specialVehicleService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IAmazonS3 s3Client)
        {
            _specialVehicleService = specialVehicleService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _s3Client = s3Client;
        }

        [HttpGet]
        public IActionResult CreateSpecialVehicle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialVehicle(SpecialVehicleViewModel vehicleViewModel, List<IFormFile> photos = null)
        {
            if (photos != null && photos.Count > 0)
            {
                var specialVehicle = _mapper.Map<SpecialVehicleModel>(vehicleViewModel);

                specialVehicle.PhotoPaths = new List<PhotoPath>();

                foreach (var photo in photos)
                {
                    if (photo.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await photo.CopyToAsync(memoryStream);
                            string uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
                            string objectKey = $"images/{uniqueFileName}";

                            var awsCredentials = new BasicAWSCredentials("AKIAS6SSBVP5XQQZBCL4", "ddMO7lxlgOlPZfvi8Zt4pSz00aSDGZrNOVF4XAgU");

                            var config = new AmazonS3Config
                            {
                                RegionEndpoint = RegionEndpoint.EUNorth1
                            };

                            using (var s3Client = new AmazonS3Client(awsCredentials, config))
                            {
                                var fileTransferUtility = new TransferUtility(s3Client);
                                await fileTransferUtility.UploadAsync(memoryStream, _bucketName, objectKey);
                            }

                            string filePath = $"https://{_bucketName}.s3.amazonaws.com/{objectKey}";

                            specialVehicle.PhotoPaths.Add(new PhotoPath { Value = filePath });
                        }
                    }
                }

                _specialVehicleService.Create(specialVehicle);
                return RedirectToAction("Index", "Home");
            }

            return View(vehicleViewModel);
        }
    }
}
