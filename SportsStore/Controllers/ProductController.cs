using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository) {
            _repository = repository;
        }

        public int PageSize { get; set; } = 2;

        public ViewResult List(string category, int productPage = 1) {
            var products =
                _repository
                    .Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId);

            return View(new ProductListViewModel {
                Products = products.Skip((productPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                },
                CurrentCategory = category
            });
        }
    }
}