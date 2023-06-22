using System.ComponentModel.DataAnnotations;

namespace SnappFoodTask.Domain.DTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 1.")]
        public int IncreaseCount { get; set; }
    }
}
