using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }

        
        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, you cart is empty");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed", new
                {
                    orderId = order.OrderId
                });
            }

            else
            {
                return View();
            }
        }
    }
}
