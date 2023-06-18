using TechConnect.Core;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.DAL.Sql
{
    public class SpecialVehicleSqlRepository : IRepository<SpecialVehicleModel>
    {

        private readonly TechConnectContext _specialVehicleContext;

        public SpecialVehicleSqlRepository(TechConnectContext specialVehicleContext)
        { 
            _specialVehicleContext = specialVehicleContext;
        }
        public void Create(SpecialVehicleModel vehicle)
        {
            _specialVehicleContext.SpecialVehicls.Add(vehicle);
            _specialVehicleContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public SpecialVehicleModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpecialVehicleModel> ReadAll()
        {
            return _specialVehicleContext.SpecialVehicls.ToList();
        }

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
