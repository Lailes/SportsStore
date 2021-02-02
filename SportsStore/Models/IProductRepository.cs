using System.Linq;

namespace SportsStore.Models {
    public interface IProductRepository {
        IQueryable<Product> Products { get; }


        public void SaveProduct(Product product);

        public Product DeleteProduct(int productId);
    }
}