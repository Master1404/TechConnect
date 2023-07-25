using Microsoft.EntityFrameworkCore;
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
            _specialVehicleContext.SpecialVehicles.Add(vehicle);
            _specialVehicleContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public SpecialVehicleModel GetById(int id)
        {
            var specialVehicle = _specialVehicleContext.SpecialVehicles
             .Include(x => x.PhotoPaths)
               .FirstOrDefault(x => x.Id == id);

            return specialVehicle;
        }

        public IEnumerable<SpecialVehicleModel> ReadAll()
        {
            return _specialVehicleContext.SpecialVehicles.ToList();
        }

        public bool Update(SpecialVehicleModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
