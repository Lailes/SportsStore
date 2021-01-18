using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
    public class AdminController : Controller {

        public IProductRepository ProductRepository { get; set; }

        public AdminController(IProductRepository productRepository) {
            ProductRepository = productRepository;
        }

        public ViewResult Index() => View(ProductRepository.Products);
    }
}