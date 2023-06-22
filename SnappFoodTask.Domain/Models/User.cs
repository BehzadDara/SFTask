using SnappFoodTask.Domain.DTOs;

namespace SnappFoodTask.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();

        public static async Task Buy(IUnitOfWork unitOfWork, BuyDTO input)
        {
            var entity = await GetUser(unitOfWork, input.UserId);
            var product = await Product.Get(unitOfWork, input.ProductId);

            product.DecreaseInventoryCountAsync(unitOfWork);
            entity.Orders.Add(new Order
            {
                Product = product,
                Buyer = entity
            });

            unitOfWork.UserRepository.Update(entity);
            unitOfWork.Complete();
        }

        public static async Task<User> GetUser(IUnitOfWork unitOfWork, int userId)
        {
            var entity = await unitOfWork.UserRepository.Get(userId);
            if (entity is null)
            {
                throw new Exception("Not found!");
            }

            return entity;
        }

    }
}
