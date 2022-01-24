using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoinStore.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}