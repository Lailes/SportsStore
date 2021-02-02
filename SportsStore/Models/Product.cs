using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models {
    public class Product {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the description")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Enter the correct price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Specify the category")]
        public string Category { get; set; }
    }
}