using System;
using System.ComponentModel.DataAnnotations;

namespace Fridge.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }
        
        [Required, MaxLength(200)]
        public string Name { get; set; }
        
        [Required, MaxLength(50)]
        public string Kind { get; set; }
        
        [Required, MaxLength(10), Display(Name="Units")]
        public string PortionUnits { get; set; }
        
        [Required, Range(0.000001d, Double.MaxValue), Display(Name="Size")]
        public decimal PortionSize { get; set; }
    }
}