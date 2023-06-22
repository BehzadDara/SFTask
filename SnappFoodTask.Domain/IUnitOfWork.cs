using SnappFoodTask.Domain.IRepositories;

namespace SnappFoodTask.Domain
{
    public interface IUnitOfWork
    {
        public void Complete();
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
