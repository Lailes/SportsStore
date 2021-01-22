using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the decripton")]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue, ErrorMessage = "Enter the correct price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Specify the category")]
        public string Category { get; set; }
    }
}