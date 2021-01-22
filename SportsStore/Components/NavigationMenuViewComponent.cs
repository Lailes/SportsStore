using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; set; }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(ProductRepository
                .Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}