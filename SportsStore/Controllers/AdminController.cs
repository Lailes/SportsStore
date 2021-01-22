using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; set; }

        public ViewResult Index()
        {
            return View(ProductRepository.Products);
        }

        [HttpGet]
        public ViewResult Edit(int productId)
        {
            return View(ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            ProductRepository.SaveProduct(product);

            TempData["message"] = $"{product.Name} is saved";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ViewResult Create(Product product)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ViewResult Delete(int productId)
        {
            throw new NotImplementedException();
        }
    }
}