using Amazon.S3.Model;
using Amazon.S3;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;
using Amazon;

namespace TechConnect.Core
{
    public class AdsService : IService<SpecialVehicleModel, int>
    {

        private readonly IRepository<SpecialVehicleModel> _specialVehicleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly string _accessKeyId = "AKIAS6SSBVP5XQQZBCL4";
        private readonly string _secretAccessKey = "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";
        private readonly string _region = "eu-north-1";
        private readonly string _bucketName = "techconnect1404";


        public AdsService(IRepository<SpecialVehicleModel> specialVehicleRepository, IWebHostEnvironment webHostEnvironment)
        {
            _specialVehicleRepository = specialVehicleRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Create(SpecialVehicleModel specialVehicle)
        {
            if (specialVehicle == null)
            {
                throw new ArgumentNullException(nameof(specialVehicle));
            }

            _specialVehicleRepository.Create(specialVehicle);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SpecialVehicleModel> GetAll()
        {
            return _specialVehicleRepository.ReadAll().ToList();
        }

        public SpecialVehicleModel GetById(int entityId)
        {
            return _specialVehicleRepository.GetById(entityId);
        }

        /*  public List<PhotoPath> GetPhotoPaths(int id)
          {
              var advertisement = _specialVehicleRepository.GetById(id);

              if (advertisement != null && advertisement.PhotoPaths != null && advertisement.PhotoPaths.Count > 0)
              {
                  var photoPaths = new List<PhotoPath>();

                  foreach (var photoPath in advertisement.PhotoPaths)
                  {
                      var imagePath = GetS3ImageUrl(photoPath.Value);
                      photoPaths.Add(new PhotoPath { Value = imagePath });
                  }

                  return photoPaths;
              }
              else
              {
                  var placeholderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "placeholder.jpg");

                  var photoPaths = new List<PhotoPath> { new PhotoPath { Value = placeholderPath } };
                  return photoPaths;
              }
          }*/

        public List<PhotoPath> GetPhotoPaths(int id)
        {
            var advertisement = _specialVehicleRepository.GetById(id);

            if (advertisement != null && advertisement.PhotoPaths != null && advertisement.PhotoPaths.Count > 0)
            {
                var photoPaths = new List<PhotoPath>();

                foreach (var photoPath in advertisement.PhotoPaths)
                {
                    // Получение относительного пути к файлу из URL
                    var uri = new Uri(photoPath.Value);
                    var relativePath = uri.AbsolutePath.TrimStart('/');

                    var imagePath = GetS3ImageUrl(relativePath);
                    photoPaths.Add(new PhotoPath { Value = imagePath });
                }

                return photoPaths;
            }
            else
            {
                var placeholderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "placeholder.jpg");

                var photoPaths = new List<PhotoPath> { new PhotoPath { Value = placeholderPath } };
                return photoPaths;
            }
        }


        private string GetS3ImageUrl(string s3Key)
        {
            var s3Client = new AmazonS3Client(_accessKeyId, _secretAccessKey, RegionEndpoint.GetBySystemName(_region));
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "techconnect1404",
                Key = s3Key,
                Expires = DateTime.Now.AddDays(1)
            };

            var url = s3Client.GetPreSignedURL(request);
            return url;
        }


        public string GetPhotoUrl(string photoPath)
        {
            throw new NotImplementedException();
        }

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }

    }
}
