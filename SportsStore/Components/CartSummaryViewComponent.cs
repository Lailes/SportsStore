using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components {
    public class CartSummaryViewComponent : ViewComponent {
        public CartSummaryViewComponent(Cart cartService) {
            CartService = cartService;
        }

        private Cart CartService { get; }

        public IViewComponentResult Invoke() {
            return View(CartService);
        }
    }
}