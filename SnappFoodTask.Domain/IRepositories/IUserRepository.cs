using SnappFoodTask.Domain.Models;

namespace SnappFoodTask.Domain.IRepositories
{
    public interface IUserRepository
    {
        public void Update(User user);
        public Task<User?> Get(int id);
    }
}
