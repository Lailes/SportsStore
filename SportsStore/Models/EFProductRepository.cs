using System.Linq;

namespace SportsStore.Models
{
    public class EfProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EfProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return;
            }

            var storedProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (storedProduct == null) return;

            storedProduct.Category = product.Category;
            storedProduct.Description = product.Description;
            storedProduct.Name = product.Name;
            storedProduct.Price = product.Price;

            _context.SaveChanges();
        }
    }
}