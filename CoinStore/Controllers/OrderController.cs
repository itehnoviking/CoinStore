using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinStore.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout()
        {
            return View(new Order());
        }
    }
}
