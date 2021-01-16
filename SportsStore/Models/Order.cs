using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models {
    public class Order {
        [BindNever]
        public int OrderID { get; set; }
        
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        
        [Required(ErrorMessage = "Enter the name. Please")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Enter the first address line. Please")]
        public string Line1 { get; set; }
        
        public string Line2 { get; set; }
        
        public string Line3 { get; set; }
        
        [Required(ErrorMessage = "Enter the city name. Please")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Enter the state. Please")]
        public string State { get; set; }
        
        public string Zip { get; set; }
        
        [Required(ErrorMessage = "Enter the country name. Please")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; } = false;
    }
}