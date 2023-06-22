using Microsoft.EntityFrameworkCore;
using SnappFoodTask.Domain.IRepositories;
using SnappFoodTask.Domain.Models;

namespace SnappFoodTask.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<User> Set => _dbContext.Set<User>();

        public void Update(User user)
        {
            Set.Update(user);
        }

        public async Task<User?> Get(int id)
        {
            return await Set.SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
