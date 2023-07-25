using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechConnect.Core;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Controllers
{
    public class AdsDetailsController : Controller
    {
        private readonly IService<SpecialVehicleModel, int> _specialVehicleService;
        private readonly IMapper _mapper;

        public AdsDetailsController(IService<SpecialVehicleModel, int> specialVehicleService, IMapper mapper)
        {
            _specialVehicleService = specialVehicleService;
            _mapper = mapper;


        }

        public IActionResult Details(int id)
        {
            var advertisement = _specialVehicleService.GetById(id);

            if (advertisement == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<SpecialVehicleViewModel>(advertisement);
            return View(viewModel);
        }

    }
}
