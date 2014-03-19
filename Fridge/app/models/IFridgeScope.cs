using System.Collections.Generic;

namespace Fridge.Models
{
    public class IFridgeScope
    {
        public List<Food> Foods { get; set; }
        public Food EditFood { get; set; }
        public string EditMode { get; set; }

        public void SubmitEdit() { }
        public void SelectEditFood(Food food) { }
        public void CreateFood() { }
        public void DeleteFood(Food food) { }
    }
}