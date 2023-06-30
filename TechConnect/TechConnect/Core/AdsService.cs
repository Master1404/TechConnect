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

             if (advertisement != null)
             {
                 // Формирование полного пути к фотографии на сервере
                 var fullPath = Path.Combine( advertisement.PhotoPaths.ToString());

                 // Создание объекта PhotoPath и добавление его в список
                 var photoPaths = new List<PhotoPath> { new PhotoPath { Value = fullPath } };

                 return photoPaths;
             }

             return new List<PhotoPath>();
         }*/
        public List<PhotoPath> GetPhotoPaths(int id)
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
        }
        /* public string GetFirstPhotoPath(int id)
         {
             var advertisement = _specialVehicleRepository.GetById(id);

             if (advertisement != null && advertisement.PhotoPaths != null && advertisement.PhotoPaths.Count > 0)
             {
                 var firstPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", advertisement.PhotoPaths[0].Value);
                 return firstPhotoPath;
             }
             else
             {
                 var placeholderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "placeholder.jpg");
                 return placeholderPath;
             }
         }*/

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
