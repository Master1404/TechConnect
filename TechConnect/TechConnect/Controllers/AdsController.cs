using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechConnect.Core;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Controllers
{
    public class AdsController : Controller
    {
        private readonly IService<SpecialVehicleModel, int> _specialVehicleService;
        private readonly IMapper _mapper;
        public AdsController(IService<SpecialVehicleModel, int> specialVehicleService, IMapper mapper)
        {
            _specialVehicleService = specialVehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateSpecialVehicle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSpecialVehicle(SpecialVehicleViewModel vehicleViewModel)
        {

            var vehicle = _mapper.Map<SpecialVehicleModel>(vehicleViewModel);
            if (ModelState.IsValid)
            {
                _specialVehicleService.Create(vehicle);
               // return RedirectToAction("GetAllUsers");
            }
            return View();
        }

      /*  [HttpPost]
        public async Task<IActionResult> CreateSpecialVehicle(SpecialVehicleViewModel vehicleViewModel)
        {
            if (vehicleViewModel.Photo != null)
            {
                // Генерируем уникальное имя файла
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vehicleViewModel.Photo.FileName);

                // Сохраняем файл на сервере
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vehicleViewModel.Photo.CopyToAsync(fileStream);
                }

                // Присваиваем свойству модели имя файла
                vehicleViewModel.Img = fileName;
            }

            var vehicle = _mapper.Map<SpecialVehicleModel>(vehicleViewModel);
            if (ModelState.IsValid)
            {
                _specialVehicleService.Create(vehicle);
                // return RedirectToAction("GetAllUsers");
            }
            return View();
        }*/

    }
}
