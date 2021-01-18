using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {
    public class OrderControllerTests {
        [Fact]
        public void CantCheckoutEmptyCart() {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();

            var target = new OrderController(mock.Object, cart);
            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }
    }
}