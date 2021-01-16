using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
    public class OrderController : Controller {
        private IOrderRepository OrderRepository { get; }
        private Cart CartService { get; }
        
        public OrderController(IOrderRepository orderRepository, Cart cart) {
            OrderRepository = orderRepository;
            CartService = cart;
        }

        [HttpGet]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) {
            if (!CartService.Lines.Any()) {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (!ModelState.IsValid) return View(order);

            order.Lines = CartService.Lines.ToArray();
            OrderRepository.SaveOrder(order);
            return RedirectToAction("Completed");
        }

        public ViewResult Completed() {
            CartService.Clear();
            return View();
        }


    }
}