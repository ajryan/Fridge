using System.Collections.Generic;
using System.Data.Entity;
using Fridge.Models;

namespace Fridge.Data
{
    public class FridgeInitializer : DropCreateDatabaseIfModelChanges<FridgeContext>
    {
        protected override void Seed(FridgeContext context)
        {
            var foods = new List<Food>
            {
                new Food { Brand="Horizon", Kind="Milk", Name="2% Milk", PortionUnits = "Gallon", PortionSize = 1.0m },
                new Food { Brand="Cypress Grove", Kind="Cheese", Name="Humboldt Fog", PortionUnits = "Pound", PortionSize = 0.5m },
            };
            foods.ForEach(f => context.Foods.Add(f));
            context.SaveChanges();
        }
    }
}