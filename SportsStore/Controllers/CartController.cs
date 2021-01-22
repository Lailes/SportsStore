using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        public CartController(IProductRepository productRepository, Cart cartService)
        {
            ProductRepository = productRepository;
            CartService = cartService;
        }

        private IProductRepository ProductRepository { get; }
        private Cart CartService { get; }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {ReturnUrl = returnUrl, Cart = CartService});
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null) CartService.AddItem(product, 1);
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null) CartService.Remove(product);
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult ClearCart(string returnUrl)
        {
            CartService.Clear();
            return RedirectToAction("Index", new CartIndexViewModel {Cart = CartService, ReturnUrl = returnUrl});
        }
    }
}