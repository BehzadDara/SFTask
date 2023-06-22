using SnappFoodTask.Domain;
using SnappFoodTask.Domain.IRepositories;
using SnappFoodTask.Infrastructure.Repositories;

namespace SnappFoodTask.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SnappFoodDBContext _dBContext;

        public UnitOfWork(SnappFoodDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void Complete()
        {
            _dBContext.SaveChanges();
        }

        private ProductRepository? productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                productRepository ??= new ProductRepository(_dBContext);
                return productRepository;
            }
        }

        private UserRepository? userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                userRepository ??= new UserRepository(_dBContext);
                return userRepository;
            }
        }

    }
}
