using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers {
    public class CartController : Controller {

        public IProductRepository ProductRepository { get; set; }

        public CartController(IProductRepository productRepository) {
            ProductRepository = productRepository;
        }

        public ViewResult Index(string returnUrl) => View(new CartIndexViewModel{
            ReturnUrl = returnUrl, 
            Cart = GetCart()
        });
        public RedirectToActionResult AddToCart(int productId, string returnUrl) {
            var product = ProductRepository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null) {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new{returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl) {
            var product = ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null) {
                var cart = GetCart();
                cart.RemoveAll(product);
                SaveCart(cart);
            }
            
            return RedirectToAction("Index", new{returnUrl});
        }

        private Cart GetCart() => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);
    }
}