﻿using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
    [Authorize]
    public class AdminController : Controller {
        public AdminController(IProductRepository productRepository) {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; }

        public ViewResult Index() {
            return View(ProductRepository.Products);
        }

        [HttpGet]
        public ViewResult Edit(int productId) {
            return View(ProductRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product) {
            if (!ModelState.IsValid) return View(product);

            ProductRepository.SaveProduct(product);

            TempData["message"] = $"{product.Name} is saved";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create() {
            return View("Edit", new Product());
        }

        [HttpPost]
        public RedirectToActionResult Delete(int productId) {
            var product = ProductRepository.DeleteProduct(productId);
            TempData["message"] =
                product != null ? $"Product \"{product.Name}\" was deleted" : "Product wasn't deleted";
            return RedirectToAction("Index");
        }
    }
}