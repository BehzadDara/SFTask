using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnappFoodTask.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int InventoryCount { get; set; }
        public decimal Price { get; set; }
    }
}
