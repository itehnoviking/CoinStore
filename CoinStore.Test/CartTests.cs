
using CoinStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoinStore.Test
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange - created several testing products

            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2" };

            //Arrange - created new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange - created several testing products
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2" };

            //Arrange - created new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines
                .OrderBy(c => c.Product.ProductId).ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);

        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange - created several testing products
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2" };
            Product p3 = new Product { ProductId = Guid.NewGuid(), Name = "P3" };

            //Arrange - created new cart
            Cart target = new Cart();

            //Arrange - added several products in cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.Removeline(p2);

            //Assert
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());

        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //Arrange - created several testing products
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2", Price = 50M };

            //Arrange - created new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.Equal(450m, result);

        }

        [Fact]
        public void Can_Clear_Contents()
        {
            //Arrange - created several testing products
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2", Price = 50M };

            //Arrange - created new cart
            Cart target = new Cart();

            //Arrange - added several products in cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);

            //Act - cart cleaning
            target.Clear();

            //Assert
            Assert.Empty(target.Lines);
        }

    }
}
