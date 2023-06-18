using TechConnect.Models;

namespace TechConnect.Core
{
    public interface IService<TEntity, TId>
        where TEntity : IRecord<TId>
        where TId : struct, IEquatable<TId>
    {
        void Create(TEntity entity);
        bool Update(TEntity entity);
        TEntity GetById(int entityId);
        List<TEntity> GetAll();
        void Delete(int id);
       
    }
}
