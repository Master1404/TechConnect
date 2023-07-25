using Amazon.S3.Model;
using Amazon.S3;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using TechConnect.Core;
using TechConnect.Migrations;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;
using Amazon;
using Amazon.Runtime;

namespace TechConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<SpecialVehicleModel, int> _specialVehicle;
        private readonly IAmazonS3 _s3Client;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        private readonly string _accessKeyId = "AKIAS6SSBVP5XQQZBCL4";
        private readonly string _secretAccessKey = "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";
        private readonly string _region = "eu-north-1";

        private readonly string _bucketName = "techconnect1404";

        public HomeController(IService<SpecialVehicleModel, int> specialVehicle, IMapper mapper, IAmazonS3 s3Client, IWebHostEnvironment webHostEnvironment)
        {
            _specialVehicle = specialVehicle;
            _mapper = mapper;
            _s3Client = s3Client;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var advertisements = _specialVehicle.GetAll();

            foreach (var advertisement in advertisements)
            {
                var photoPaths = _specialVehicle.GetPhotoPaths(advertisement.Id);

                var formFiles = new List<IFormFile>();
                foreach (var photoPath in photoPaths)
                {
                    var imageBytes = await LoadImageFromS3(_bucketName, photoPath.Value);

                   // var fileName = Path.GetFileName(photoPath.Value);
                    var fileName = Path.GetFileName(new Uri(photoPath.Value).AbsolutePath);
                    var formFile = new FormFile(new MemoryStream(imageBytes), 0, imageBytes.Length, "name", fileName);
                    formFiles.Add(formFile);
                }

                advertisement.Photos = formFiles;
            }

            var viewModel = _mapper.Map<List<SpecialVehicleViewModel>>(advertisements);
            return View(viewModel);
        }

        private async Task<byte[]> LoadImageFromS3(string s3BucketName, string s3Key)
        {
            var awsCredentials = new BasicAWSCredentials(_accessKeyId, _secretAccessKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_region)
            };

            using (var client = new AmazonS3Client(awsCredentials, config))
            {
                // Извлекаем относительный путь из URL
                var uri = new Uri(s3Key);
                var relativePath = uri.AbsolutePath.TrimStart('/');
                //var relativePath = s3Key.Split('/').Last();
                var request = new GetObjectRequest
                {
                    BucketName = s3BucketName,
                    Key = relativePath
                };

                using (var response = await client.GetObjectAsync(request))
                {
                    using (var stream = response.ResponseStream)
                    {  
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdsByCategory(string category)
        {
            if (Enum.TryParse(category, out AdsCategory adsCategory))
            {
                // Получите список объявлений для выбранной категории
                var advertisements = _specialVehicle.GetAll().Where(ad => ad.Category == adsCategory).ToList();

                // Заполните свойство Photos каждого объявления
                foreach (var advertisement in advertisements)
                {
                    var photoPaths = _specialVehicle.GetPhotoPaths(advertisement.Id);
                    var formFiles = new List<IFormFile>();
                    foreach (var photoPath in photoPaths)
                    {
                        var imageBytes = await LoadImageFromS3(_bucketName, photoPath.Value);

                        var fileName = Path.GetFileName(photoPath.Value);

                        var formFile = new FormFile(new MemoryStream(imageBytes), 0, imageBytes.Length, "name", fileName);
                        formFiles.Add(formFile);
                    }
                    advertisement.Photos = formFiles;
                }

                // Маппинг объявлений на модель представления
                var viewModel = _mapper.Map<List<SpecialVehicleViewModel>>(advertisements);

                // Верните объявления в формате JSON
                return Json(viewModel);
            }
            else
            {
                // Если передана недопустимая категория, верните пустой результат
                return Json(new List<SpecialVehicleViewModel>());
            }
        }



        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}