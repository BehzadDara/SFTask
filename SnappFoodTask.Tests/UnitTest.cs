using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Moq;
using SnappFoodTask.Domain;
using SnappFoodTask.Domain.DTOs;
using SnappFoodTask.Domain.IRepositories;
using SnappFoodTask.Domain.Models;
using SnappFoodTask.Infrastructure;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SnappFoodTask.Tests
{
    public class UnitTest
    {
        private readonly UnitOfWork _unitOfWork;

        public UnitTest()
        {
            var option = new DbContextOptionsBuilder<SnappFoodDBContext>()
                .UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Initial Catalog = SnappFoodDB")
                .Options;
            var context = new SnappFoodDBContext(option);

            _unitOfWork = new UnitOfWork(context);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetProductTest(int id)
        {
            var entity = await Product.Get(_unitOfWork, id);
            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);
            Assert.Equal("Init product", entity.Title);
            Assert.Equal(100, entity.Price);
            Assert.Equal(20, entity.Discount);
        }

        [Theory]
        [InlineData(1,2)]
        public async Task IncreaseInventoryTest(int id, int increaseCount)
        {
            var inventoryCount = (await Product.Get(_unitOfWork, id)).InventoryCount;

            await Product.IncreaseInventoryCountAsync(_unitOfWork, new UpdateProductDTO
            {
                Id = id,
                IncreaseCount = increaseCount
            });

            var entity = await Product.Get(_unitOfWork, id);

            Assert.Equal(inventoryCount + increaseCount, entity.InventoryCount);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task AddOrderTest(int userId, int productId)
        {
            var inventoryCount = (await Product.Get(_unitOfWork, productId)).InventoryCount;

            await User.Buy(_unitOfWork, new BuyDTO
            {
                UserId = userId,
                ProductId = productId
            });

            var lastOrder = (await User.GetUser(_unitOfWork, userId)).Orders.LastOrDefault();
            Assert.NotNull(lastOrder);
            Assert.Equal(productId, lastOrder.Product.Id);

            var product = await Product.Get(_unitOfWork, productId);
            Assert.Equal(inventoryCount - 1, product.InventoryCount);

        }

    }
}