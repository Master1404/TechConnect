namespace TechConnect.Core
{
    public interface IRepository<TEntity>
    {
        void Create(TEntity entity);
        IEnumerable<TEntity> ReadAll();
        TEntity GetById(int id);
        bool Update(TEntity entity);
        bool DeleteById(int id);
    }
}
