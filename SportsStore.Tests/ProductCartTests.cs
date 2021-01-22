using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductCartTests
    {
        [Fact]
        public void TestAddNewLine()
        {
            var productCart = new Cart();
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 1);
            productCart.AddItem(new Product {Price = 110, ProductId = 2}, 1);
            productCart.AddItem(new Product {Price = 100, ProductId = 3}, 2);
            productCart.AddItem(new Product {Price = 112, ProductId = 4}, 2);

            Assert.Equal(4, productCart.Lines.Count());
        }

        [Fact]
        public void TestAddQuantity()
        {
            var productCart = new Cart();
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 1);
            productCart.AddItem(new Product {Price = 110, ProductId = 2}, 1);
            productCart.AddItem(new Product {Price = 100, ProductId = 3}, 2);
            productCart.AddItem(new Product {Price = 112, ProductId = 4}, 2);

            Assert.Equal(4, productCart.Lines.Count());

            productCart.AddItem(new Product {ProductId = 1}, 2);
            productCart.AddItem(new Product {ProductId = 2}, 3);
            productCart.AddItem(new Product {ProductId = 3}, 4);
            productCart.AddItem(new Product {ProductId = 4}, 5);

            Assert.Equal(4, productCart.Lines.Count());

            var lines = productCart.Lines.ToArray();

            Assert.Equal(3, lines[0].Quantity);
            Assert.Equal(4, lines[1].Quantity);
            Assert.Equal(6, lines[2].Quantity);
            Assert.Equal(7, lines[3].Quantity);
        }

        [Fact]
        public void TestTotalCalc()
        {
            var productCart = new Cart();
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 1);
            productCart.AddItem(new Product {Price = 110, ProductId = 2}, 1);
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 2);
            productCart.AddItem(new Product {Price = 112, ProductId = 4}, 2);


            Assert.Equal(634, productCart.CalculateTotalPrice());
        }

        [Fact]
        public void TestClear()
        {
            var productCart = new Cart();
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 1);
            productCart.AddItem(new Product {Price = 110, ProductId = 2}, 1);
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 2);
            productCart.AddItem(new Product {Price = 112, ProductId = 4}, 2);

            Assert.Equal(3, productCart.Lines.Count());

            productCart.Clear();

            Assert.Empty(productCart.Lines);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void TestRemoveAll(int productIdForRemove)
        {
            var productCart = new Cart();
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 1);
            productCart.AddItem(new Product {Price = 110, ProductId = 2}, 1);
            productCart.AddItem(new Product {Price = 100, ProductId = 1}, 2);
            productCart.AddItem(new Product {Price = 112, ProductId = 4}, 2);

            Assert.Equal(3, productCart.Lines.Count());

            productCart.RemoveAll(new Product {ProductId = productIdForRemove});

            Assert.Empty(productCart.Lines.Where(p => p.Product.ProductId == productIdForRemove));
        }
    }
}