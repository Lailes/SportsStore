using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            OrderRepository = orderRepository;
            CartService = cart;
        }

        private IOrderRepository OrderRepository { get; }
        private Cart CartService { get; }


        public ViewResult List()
        {
            return View(OrderRepository.Orders.Where(o => !o.Shipped));
        }

        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            var order = OrderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                OrderRepository.SaveOrder(order);
            }

            return RedirectToAction("List");
        }


        [HttpGet]
        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!CartService.Lines.Any()) ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (!ModelState.IsValid) return View(order);

            order.Lines = CartService.Lines.ToArray();
            OrderRepository.SaveOrder(order);
            return RedirectToAction("Completed");
        }

        public ViewResult Completed()
        {
            CartService.Clear();
            return View();
        }
    }
}