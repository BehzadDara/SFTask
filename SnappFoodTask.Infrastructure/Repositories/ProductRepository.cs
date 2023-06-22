using Microsoft.EntityFrameworkCore;
using SnappFoodTask.Domain.IRepositories;
using SnappFoodTask.Domain.Models;

namespace SnappFoodTask.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _dbContext;

        public ProductRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<Product> Set => _dbContext.Set<Product>();

        public void Add(Product product)
        {
            Set.Add(product);
        }

        public void Update(Product product)
        {
            Set.Update(product);
        }

        public async Task<Product?> Get(int id)
        {
            return await Set.SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
