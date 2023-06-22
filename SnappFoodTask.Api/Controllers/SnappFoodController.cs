using Microsoft.AspNetCore.Mvc;
using SnappFoodTask.Domain;
using SnappFoodTask.Domain.DTOs;
using SnappFoodTask.Domain.Models;
using SnappFoodTask.Domain.ViewModels;
using SnappFoodTask.Infrastructure;

namespace SnappFoodTask.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SnappFoodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SnappFoodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task AddProduct([FromBody] AddProductDTO input)
        {
            await Product.Create(_unitOfWork, input);
        }

        [HttpPut]
        public async Task UpdateProduct([FromBody] UpdateProductDTO input)
        {
            await Product.IncreaseInventoryCountAsync(_unitOfWork, input);
        }

        [HttpGet]
        public async Task<ProductViewModel> Get(int id)
        {
            return await Product.GetViewModel(_unitOfWork, id);
        }

        [HttpPost]
        public async Task Buy([FromBody] BuyDTO input)
        {
            await Domain.Models.User.Buy(_unitOfWork, input);
        }

    }
}
