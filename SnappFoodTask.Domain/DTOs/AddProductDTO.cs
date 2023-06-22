using SnappFoodTask.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SnappFoodTask.Domain.DTOs
{
    public class AddProductDTO
    {
        [CustomLength(max:40)]
        public string Title { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public int InventoryCount { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public decimal Price { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public float Discount { get; set; }
    }
}
