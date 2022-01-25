using CoinStore.Models;
using System;
using Moq;
using Xunit;
using System.Linq;
using CoinStore.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CoinStore.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = Guid.NewGuid(), Name = "P1"},
                new Product{ProductId = Guid.NewGuid(), Name = "P2"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            //Act
            IEnumerable<Product> result = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Product>;

            //Assert
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = Guid.NewGuid(), Name = "P1"},
                new Product{ProductId = Guid.NewGuid(), Name = "P2"},
                new Product{ProductId = Guid.NewGuid(), Name = "P3"},
                new Product{ProductId = Guid.NewGuid(), Name = "P4"},
                new Product{ProductId = Guid.NewGuid(), Name = "P5"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 2;

            //Act
            IEnumerable<Product> result = (controller.Index(2) as ViewResult).ViewData.Model as IEnumerable<Product>;

            //Assert
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

    }
}
