using System.Collections.Generic;

namespace Fridge.Models
{
    public class FridgeViewModel
    {
        public List<Food> AvailableFoods { get; set; }
        public List<StockItem> FoodInStock { get; set; } 
    }
}