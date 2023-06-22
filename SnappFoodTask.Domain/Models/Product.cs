using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SnappFoodTask.Domain.DTOs;
using SnappFoodTask.Domain.ViewModels;

namespace SnappFoodTask.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int InventoryCount { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }


        private static readonly MemoryCache _memoryCache = new(new MemoryCacheOptions());

        public static Task Create(IUnitOfWork unitOfWork, AddProductDTO input)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddProductDTO, Product>();
            });
            var mapper = config.CreateMapper();

            var entity = mapper.Map<AddProductDTO, Product>(input);
            unitOfWork.ProductRepository.Add(entity);
            unitOfWork.Complete();

            return Task.CompletedTask;
        }

        public static async Task IncreaseInventoryCountAsync(IUnitOfWork unitOfWork, UpdateProductDTO input)
        {
            var entity = await Get(unitOfWork, input.Id);

            _memoryCache.Remove(entity.Id);

            entity.InventoryCount += input.IncreaseCount;
            unitOfWork.ProductRepository.Update(entity);
            unitOfWork.Complete();

        }

        public void DecreaseInventoryCountAsync(IUnitOfWork unitOfWork)
        {
            if (InventoryCount == 0)
                throw new Exception("InventoryCount is 0.");

            _memoryCache.Remove(Id);

            InventoryCount--;
            unitOfWork.ProductRepository.Update(this);
        }

        public static async Task<Product> Get(IUnitOfWork unitOfWork, int productId)
        {
            var entity = _memoryCache.Get(productId) as Product;

            if (entity is null)
            {
                entity = await unitOfWork.ProductRepository.Get(productId);
                if (entity is null)
                {
                    throw new Exception("Not found!");
                }

                _memoryCache.Set(productId, entity, DateTimeOffset.Now.AddMinutes(10));
            }

            return entity;
        }

        public static async Task<ProductViewModel> GetViewModel(IUnitOfWork unitOfWork, int productId)
        {
            var entity = await Get(unitOfWork, productId);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(x => x.Price, opt => opt.MapFrom(c => c.Price * (decimal)(1 - c.Discount / 100)));
            });
            var mapper = config.CreateMapper();

            return mapper.Map<Product, ProductViewModel>(entity);
        }

    }
}
