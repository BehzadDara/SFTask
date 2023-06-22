namespace SnappFoodTask.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; } = new();
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public User Buyer { get; set; } = new();
    }
}
