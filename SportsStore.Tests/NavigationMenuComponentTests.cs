using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {
    public class NavigationMenuComponentTests {
        [Fact]
        public void CanSelectCategories() {
            var repo = new Mock<IProductRepository>();
            repo.SetupGet(r => r.Products).Returns(() => new Product[]{
                new(){ProductId = 1, Category = "Apples", Name = "P1"},
                new(){ProductId = 1, Category = "Apples", Name = "P2"},
                new(){ProductId = 1, Category = "Plums", Name = "P3"},
                new(){ProductId = 1, Category = "Oranges", Name = "P4"}
            }.AsQueryable());

            var component = new NavigationMenuViewComponent(repo.Object);
            var results =
                (((ViewViewComponentResult) component.Invoke()).ViewData.Model as IEnumerable<string>).ToArray();
            Assert.True(new[]{"Apples", "Oranges", "Plums"}.SequenceEqual(results));
        }

        [Fact]
        public void IndicatesSelectedCategory() {
            const string categoryToSelect = "apples";

            var mock = new Mock<IProductRepository>();
            mock
                .SetupGet(m => m.Products)
                .Returns(() => new Product[]{
                    new(){Category = "apples", Name = "P1"},
                    new(){Category = "oranges", Name = "P2"}
                }.AsQueryable());

            var component = new NavigationMenuViewComponent(mock.Object){
                ViewComponentContext = new ViewComponentContext{
                    ViewContext = new ViewContext{RouteData = new RouteData()}
                }
            };

            component.RouteData.Values["category"] = categoryToSelect;

            var result = (component.Invoke() as ViewViewComponentResult)?.ViewData["SelectedCategory"];
            Assert.Equal(categoryToSelect, result);
        }
    }
}