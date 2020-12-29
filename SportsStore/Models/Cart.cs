using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models {
    public class Cart {
        private List<CartLine> _line = new();

        public virtual void AddItem(Product product, int quantity) {
            var cartLine = _line.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (cartLine == null)
                _line.Add(new CartLine{
                    Quantity = quantity,
                    Product = product
                });
            else
                cartLine.Quantity += quantity;
        }

        public virtual void RemoveAll(Product product) {
            _line.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public virtual decimal CalculateTotalPrice() {
            return _line.Sum(l => l.Product.Price * l.Quantity);
        }

        public virtual void Clear() {
            _line.Clear();
        }

        public virtual IEnumerable<CartLine> Lines => _line;
    }

    public class CartLine {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}