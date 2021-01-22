using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class FakeProductRepository /* : IProductRepository */
    {
        private readonly List<Product> _products = new()
        {
            new Product {Name = "Football", Price = 25},
            new Product {Name = "Surf Board", Price = 179},
            new Product() {Name = "Running Shoes", Price = 95}
        };

        public IQueryable<Product> Products => _products.AsQueryable();

        public void SaveProduct(Product product)
        {
            _products.Add(product);
        }
    }
}