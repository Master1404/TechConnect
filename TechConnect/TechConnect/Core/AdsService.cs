using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Core
{
    public class AdsService : IService<SpecialVehicleModel, int>
    {

        private readonly IRepository<SpecialVehicleModel> _specialVehicleRepository;

        public AdsService(IRepository<SpecialVehicleModel> specialVehicleRepository)
        {
            _specialVehicleRepository = specialVehicleRepository;
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
            throw new NotImplementedException();
        }

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
