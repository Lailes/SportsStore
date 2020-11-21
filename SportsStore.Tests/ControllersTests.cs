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
                .Returns(new[] {
                    new Product{ProductId = 1, Name = "P1"},
                    new Product{ProductId = 2, Name = "P2"},
                    new Product{ProductId = 3, Name = "P3"},
                    new Product{ProductId = 4, Name = "P4"},
                    new Product{ProductId = 5, Name = "P5"},
                }.AsQueryable());


            var productController = new ProductController(mockRepo.Object) {PageSize = 3};

            var viewDataModel = productController.List(2).ViewData.Model as ProductListViewModel;

            if (viewDataModel == null) return;
            
            var pageInfo = viewDataModel.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}