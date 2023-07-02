using AutoMapper;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using TechConnect.Core;
using TechConnect.Migrations;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IMapper _mapper;
        private readonly IService<SpecialVehicleModel, int> _specialVehicle;

        public HomeController(IService<SpecialVehicleModel, int> specialVehicle, IMapper mapper)
        {
            _specialVehicle = specialVehicle;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            /* var advertisements = _specialVehicle.GetAll();

             foreach (var advertisement in advertisements)
             {
                 var photoPaths = _specialVehicle.GetPhotoPaths(advertisement.Id); // Получите пути к фотографиям для данного объявления

                 // advertisement.PhotoPaths = photoPaths.Cast<PhotoPath>().ToList();
                 advertisement.PhotoPaths = photoPaths.Select(path => new PhotoPath { Value = path.ToString() }).ToList();
             }

             var viewModel = _mapper.Map<List<SpecialVehicleViewModel>>(advertisements);
             return View(viewModel);*/

            var advertisements = _specialVehicle.GetAll();

            foreach (var advertisement in advertisements)
            {
                var photoPaths = _specialVehicle.GetPhotoPaths(advertisement.Id); // Получите пути к фотографиям для данного объявления

                var formFiles = new List<IFormFile>();
                foreach (var photoPath in photoPaths)
                {
                    // Создайте объект IFormFile из пути к фотографии
                    var formFile = new FormFile(new MemoryStream(), 0, 0, "name", photoPath.Value);
                    formFiles.Add(formFile);
                }

                advertisement.Photos = formFiles;
            }

            var viewModel = _mapper.Map<List<SpecialVehicleViewModel>>(advertisements);
            return View(viewModel);
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