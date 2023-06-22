using SnappFoodTask.Domain.Models;

namespace SnappFoodTask.Domain.IRepositories
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public void Update(Product product);
        public Task<Product?> Get(int id);
    }
}
