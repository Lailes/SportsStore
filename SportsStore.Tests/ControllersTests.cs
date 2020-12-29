using System.Linq;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests {
    public class ControllersTests {
        [Fact]
        public void ListTest() {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products)
                .Returns(new[]{
                    new Product{ProductId = 1, Name = "P1"},
                    new Product{ProductId = 2, Name = "P2"},
                    new Product{ProductId = 3, Name = "P3"},
                    new Product{ProductId = 4, Name = "P4"},
                    new Product{ProductId = 5, Name = "P5"}
                }.AsQueryable());

            var productController = new ProductController(mockRepo.Object){PageSize = 3};

            if (productController.List(null, 2).ViewData.Model is not ProductListViewModel viewDataModel) return;

            var pageInfo = viewDataModel.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void CanFilter() {
            var mock = new Mock<IProductRepository>();
            mock.Setup(repo => repo.Products).Returns(new Product[]{
                new(){ProductId = 1, Name = "P1", Category = "Cat1"},
                new(){ProductId = 2, Name = "P2", Category = "Cat2"},
                new(){ProductId = 3, Name = "P3", Category = "Cat1"},
                new(){ProductId = 4, Name = "P4", Category = "Cat2"},
                new(){ProductId = 5, Name = "P5", Category = "Cat3"}
            }.AsQueryable());

            var productController = new ProductController(mock.Object){
                PageSize = 3
            };

            var result = ((ProductListViewModel) productController.List("Cat2").ViewData.Model).Products.ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void CanCategorySpecialCount() {
            var mock = new Mock<IProductRepository>();
            mock
                .SetupGet(p => p.Products)
                .Returns(() => new Product[]{
                    new(){Category = "C1", Name = "P1"},
                    new(){Category = "C1", Name = "P2"},
                    new(){Category = "C2", Name = "P3"},
                    new(){Category = "C3", Name = "P4"},
                    new(){Category = "C2", Name = "P5"},
                    new(){Category = "C1", Name = "P5"}
                }.AsQueryable());

            var controller = new ProductController(mock.Object){
                PageSize = 10
            };

            var c1Count = ((ProductListViewModel) controller.List("C1").Model).PagingInfo.TotalItems;
            var c2Count = ((ProductListViewModel) controller.List("C2").Model).PagingInfo.TotalItems;
            var c3Count = ((ProductListViewModel) controller.List("C3").Model).PagingInfo.TotalItems;
            var countTotal = ((ProductListViewModel) controller.List(null).Model).PagingInfo.TotalItems;

            Assert.Equal(3, c1Count);
            Assert.Equal(2, c2Count);
            Assert.Equal(1, c3Count);
            Assert.Equal(6, countTotal);
        }
    }
}