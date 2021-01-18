using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {
    public class AdminControllerTests {


        [Fact]
        public void TestList() {
            var mock = new Mock<IProductRepository>();
            mock
                .SetupGet(o => o.Products)
                .Returns(() => new Product[] {
                new() {Category = "C1", Name = "P1"},
                new() {Category = "C3", Name = "P2"},
                new() {Category = "C2", Name = "P3"},
                new() {Category = "C4", Name = "P4"},
            }.AsQueryable());


            var adminController = new AdminController(mock.Object);

            var result = (adminController.Index().Model as IEnumerable<Product> ?? Array.Empty<Product>()).ToArray();
            
            Assert.Equal(4, result.Length);
            
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
            Assert.Equal("P4", result[3].Name);
        }
        
    }
}