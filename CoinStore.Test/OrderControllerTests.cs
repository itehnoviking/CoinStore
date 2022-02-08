using CoinStore.Controllers;
using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoinStore.Test
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - create imitation repository of data
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            //Arrange - create a empty cart
            Cart cart = new Cart();

            //Arrange - create order
            Order order = new Order();

            //Arrange - create instance of controller
            OrderController target = new OrderController(mock.Object, cart);

            //Act
            ViewResult result = target.Checkout(order) as ViewResult;

            //Assert - check that the order as not been saved
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Assert - checking that a method returns a standart representation
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Assert - checking that an invalid model was passed to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange - create imitation repository of data
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            //Arrange - create a cart with one element
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - create instance of controller
            OrderController target = new OrderController(mock.Object, cart);

            //Arrange - added exception in model
            target.ModelState.AddModelError("error", "error");

            //Act - transition to payment
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //Assert - check that the order as not been saved
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Assert - checking that a method returns a standart representation
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Assert - checking that an invalid model was passed to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange - create imitation repository of data
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            //Arrange - create a cart with one element
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - create instance of controller
            OrderController target = new OrderController(mock.Object, cart);

            //Act - transition to payment
            RedirectToPageResult result = target.Checkout(new Order()) as RedirectToPageResult;


            //Assert - check that the order as not been saved
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            //Assert - checking that the method is redirected to the Completed action
            Assert.Equal("/Completed", result.PageName);


        }
    }
}
