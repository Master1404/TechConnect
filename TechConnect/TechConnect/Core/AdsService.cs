using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Core
{
    public class AdsService : IService<SpecialVehicleModel, int>
    {

        private readonly IRepository<SpecialVehicleModel> _specialVehicleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
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

        /* public List<PhotoPath> GetPhotoPaths(int id)
         {
             var advertisement = _specialVehicleRepository.GetById(id);

             if (advertisement != null && advertisement.PhotoPaths != null && advertisement.PhotoPaths.Count > 0)
             {
                 var photoPaths = new List<PhotoPath>();

                 foreach (var photoPath in advertisement.PhotoPaths)
                 {
                     var imagePath = Path.Combine("images", photoPath.Value);
                     photoPaths.Add(new PhotoPath { Value = imagePath });
                 }

                 return photoPaths;
             }
             else
             {
                 // var placeholderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "images", "placeholder.jpg");
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
                    var imagePath = GetGoogleCloudStorageUrl(photoPath.Value);
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

        private string GetGoogleCloudStorageUrl(string fileName)
        {

            /* var jsonKeyFilePath = "D:/TechConnect/TechConnect/TechConnect/TechConnect/wwwroot/json_google/helical-door-391409-e15df7055dad.json";
            // var jsonKeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json_google", "helical-door-391409-e15df7055dad.json");
             var bucketName = "techconnect";

             var credential = GoogleCredential.FromFile(jsonKeyFilePath);
             var storageClient = StorageClient.Create(credential);
             var storageObject = storageClient.GetObject(bucketName, fileName);
             var url = $"https://storage.googleapis.com/{bucketName}/{fileName}";
             return url;*/


            
            return fileName;
        }

        public string GetPhotoUrl(string photoPath)
        {
            throw new NotImplementedException();
        }

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }

       /* List<string> IService<SpecialVehicleModel, int>.GetPhotoPaths(int advertisementId)
        {
            throw new NotImplementedException();
        }*/
    }
}
