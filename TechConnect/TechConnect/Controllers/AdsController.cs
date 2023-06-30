using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TechConnect.Core;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;
using Microsoft.AspNetCore.Hosting;

namespace TechConnect.Controllers
{
    public class AdsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IService<SpecialVehicleModel, int> _specialVehicleService;
        private readonly IMapper _mapper;
        public AdsController(IService<SpecialVehicleModel, int> specialVehicleService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _specialVehicleService = specialVehicleService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName.TrimStart('\\', '/'));

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await photo.CopyToAsync(fileStream);
                            }

                            specialVehicle.PhotoPaths.Add(new PhotoPath { Value = filePath });
                        }
                    }
                }

                _specialVehicleService.Create(specialVehicle);
                return RedirectToAction("Index", "Home"); // Перенаправление на другую страницу после сохранения
            }

            return View(vehicleViewModel);
        }

    }

}
