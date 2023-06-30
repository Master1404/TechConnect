using Microsoft.EntityFrameworkCore;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.DAL.Sql
{
    public class TechConnectContext : DbContext
    {
        public TechConnectContext(DbContextOptions<TechConnectContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SpecialVehicleModel> SpecialVehicles { get; set; }
    }
    
}
