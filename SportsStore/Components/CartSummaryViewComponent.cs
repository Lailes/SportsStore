using System;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components {
    public class CartSummaryViewComponent: ViewComponent {
        private Cart CartService { get; set; }
        
        public CartSummaryViewComponent(Cart cartService) => CartService = cartService;
        
        public IViewComponentResult Invoke() => View(CartService);
    }
}