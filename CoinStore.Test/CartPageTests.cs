using CoinStore.Models;
using CoinStore.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CoinStore.Test
{
    public class CartPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            //Arrange - created several testing products
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "P1"};
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "P2"};

            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] { p1, p2 }).AsQueryable<Product>());

            //Arrange - created new cart
            Cart testCart = new Cart();

            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            //Act
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);cartModel.OnGet("myUrl");

            //Assert
            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            //Arrange - created several testing products
            Guid guid = new Guid("BF7307B661FC4D58A2111C8B381D7DE8");
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductId = guid,
                    Name = "P1"
                }

            }).AsQueryable<Product>());

            //Arrange - created new cart
            Cart testCart = new Cart();

            //Act
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);

            cartModel.OnPost(guid, "myUrl");

            //Assert
            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);


        }
    }
}
