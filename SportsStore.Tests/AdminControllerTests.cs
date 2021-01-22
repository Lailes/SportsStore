using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void TestList()
        {
            var mock = new Mock<IProductRepository>();
            mock
                .SetupGet(o => o.Products)
                .Returns(() => new Product[]
                {
                    new() {Category = "C1", Name = "P1"},
                    new() {Category = "C3", Name = "P2"},
                    new() {Category = "C2", Name = "P3"},
                    new() {Category = "C4", Name = "P4"}
                }.AsQueryable());


            var adminController = new AdminController(mock.Object);

            var result = (adminController.Index().Model as IEnumerable<Product> ?? Array.Empty<Product>()).ToArray();

            Assert.Equal(4, result.Length);

            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
            Assert.Equal("P4", result[3].Name);
        }

        [Fact]
        public void CanEditProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.Products)
                .Returns(() => new Product[]
                {
                    new() {Name = "C1", ProductId = 0},
                    new() {Name = "C2", ProductId = 1},
                    new() {Name = "C3", ProductId = 2},
                    new() {Name = "C4", ProductId = 3}
                }.AsQueryable());

            var adminController = new AdminController(mock.Object);
            var p1 = GetViewModel<Product>(adminController.Edit(0));
            var p2 = GetViewModel<Product>(adminController.Edit(1));
            var p3 = GetViewModel<Product>(adminController.Edit(2));
            var p4 = GetViewModel<Product>(adminController.Edit(3));

            Assert.Equal("C1", p1.Name);
            Assert.Equal("C2", p2.Name);
            Assert.Equal("C3", p3.Name);
            Assert.Equal("C4", p4.Name);
        }

        [Fact]
        public void CantEditNotExistingProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.Products)
                .Returns(() => new Product[]
                {
                    new() {Name = "C1", ProductId = 0},
                    new() {Name = "C2", ProductId = 1},
                    new() {Name = "C4", ProductId = 3}
                }.AsQueryable());

            var adminController = new AdminController(mock.Object);

            var p2 = GetViewModel<Product>(adminController.Edit(2));
            var p5 = GetViewModel<Product>(adminController.Edit(4));

            Assert.Null(p2);
            Assert.Null(p5);
        }


        [Fact]
        public void CanSaveValidChanges()
        {
            var mock = new Mock<IProductRepository>();
            var mockDataDict = new Mock<ITempDataDictionary>();

            var target = new AdminController(mock.Object) {TempData = mockDataDict.Object};

            var product = new Product {Name = "Test"};

            var actionResult = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product), Times.Once);
            Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Equal("Index", (actionResult as RedirectToActionResult)?.ActionName);
        }

        [Fact]
        public void CanNotSaveInvalidChanges()
        {
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);

            var product = new Product {Name = "P1"};
            controller.ModelState.AddModelError("test", "Test Error");

            var result = controller.Edit(product);

            mock.Verify(m => m.SaveProduct(product), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        private T GetViewModel<T>(ViewResult result) where T : class
        {
            return result.ViewData.Model as T;
        }
    }
}