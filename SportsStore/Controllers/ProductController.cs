using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository) {
            _repository = repository;
        }

        public ViewResult List() {
            return View(_repository.Products);
        }
    }
}