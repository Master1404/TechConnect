using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechConnect.Core;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService <User, int> _userService;
        private readonly IMapper _mapper;
        private readonly IService<SpecialVehicleModel, int> _specialVehicle;

        public HomeController(IService<User, int> userService, IService<SpecialVehicleModel, int> specialVehicle, IMapper mapper)
        {
            _specialVehicle = specialVehicle;
            _userService = userService;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            var advertisements = _specialVehicle.GetAll(); // Получение списка объявлений из сервиса или репозитория

            var viewModel = _mapper.Map<List<SpecialVehicleViewModel>>(advertisements);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateUser() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserViewModel userViewModel)
        {

            var user = _mapper.Map<User>(userViewModel);
            if (ModelState.IsValid)
            {
                 _userService.Create(user);
                return RedirectToAction("GetAllUsers");
            }
            return View();
        }

        public IActionResult GetAllUsers() 
        {
            var users = _userService.GetAll();
            return View(users);
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