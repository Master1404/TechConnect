using TechConnect.Core;
using TechConnect.Models;

namespace TechConnect.DAL.Sql
{
    public class UserSqlRepository : IRepository<User>
    {
        private readonly TechConnectContext _userContext;

        public UserSqlRepository(TechConnectContext userContext)
        { 
            _userContext = userContext;
        }
        public void Create(User user)
        {
           _userContext.Users.Add(user); 
            _userContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ReadAll()
        {
            return _userContext.Users.ToList();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
