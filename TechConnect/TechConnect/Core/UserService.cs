using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

namespace TechConnect.Core
{
    public class UserService : IService<User, int>
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository) 
        {
            _userRepository = userRepository;
        }
        public void Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _userRepository.Create(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return _userRepository.ReadAll().ToList();
        }

        public User GetById(int entityId)
        {
            throw new NotImplementedException();
        }

      /*  public string GetFirstPhotoPath(int id)
        {
            throw new NotImplementedException();
        }*/

        public List<PhotoPath> GetPhotoPaths(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
