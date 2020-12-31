using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers {
    public class CartController : Controller {

        private IProductRepository ProductRepository { get; set; }
        private Cart CartService { get; set; }
        
        public CartController(IProductRepository productRepository, Cart cartService) {
            ProductRepository = productRepository;
            CartService = cartService;
        }

        public ViewResult Index(string returnUrl) => View(new CartIndexViewModel{
            ReturnUrl = returnUrl, 
            Cart = CartService
        });
        public RedirectToActionResult AddToCart(int productId, string returnUrl) {
            var product = ProductRepository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null) {
                CartService.AddItem(product, 1);
            }

            return RedirectToAction("Index", new{returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl) {
            var product = ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null) {
                CartService.RemoveAll(product);
            }
            
            return RedirectToAction("Index", new{returnUrl});
        }

        public RedirectToActionResult ClearCart(string returnUrl) {
            CartService.Clear();
            return RedirectToAction("Index", new CartIndexViewModel{Cart = CartService, ReturnUrl = returnUrl});
        }
    }
}