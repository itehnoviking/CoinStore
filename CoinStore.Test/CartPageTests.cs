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

            //Arrange - creating a simulated page and session context
            Mock<ISession> mockSession = new Mock<ISession>();
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);

            //Act
            CartModel cartModel = new CartModel(mockRepo.Object)
            {
                PageContext = new PageContext(new ActionContext
                {
                    HttpContext = mockContext.Object,
                    RouteData = new RouteData(),
                    ActionDescriptor = new PageActionDescriptor()
                })
            };

            cartModel.OnGet("myUrl");

            //Assert
            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        //[Fact]
        //public void Can_Update_Cart()
        //{
        //    //Arrange - created several testing products
        //    Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
        //    mockRepo.Setup(m => m.Products).Returns((new Product[]
        //    {
        //        new Product
        //        {
        //            ProductId = Guid.NewGuid(),
        //            Name = "P1"
        //        }

        //    }).AsQueryable<Product>());

        //    //Arrange - created new cart
        //    Cart testCart = new Cart();

        //    //Arrange - creating a simulated page and session context
        //    Mock<ISession> mockSession = new Mock<ISession>();
        //    mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
        //        .Callback<string, byte[]>((key, val) =>
        //        {
        //            testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val));
        //        });

        //    Mock<HttpContext> mockContext = new Mock<HttpContext>();
        //    mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);

        //    //Act
        //    CartModel cartModel = new CartModel(mockRepo.Object)
        //    {
        //        PageContext = new PageContext(new ActionContext
        //        {
        //            HttpContext = mockContext.Object,
        //            RouteData = new RouteData(),
        //            ActionDescriptor = new PageActionDescriptor()
        //        })
        //    };

        //    cartModel.OnPost (Product.InitialDatabase., "myUrl");


        //}
    }
}
