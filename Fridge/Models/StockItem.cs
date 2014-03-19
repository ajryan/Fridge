using System;

namespace Fridge.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public DateTime Expiration { get; set; }

        public virtual Food Food { get; set; }
    }
}