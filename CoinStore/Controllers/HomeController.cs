using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoinStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 3;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index (int productPage = 1)
        {
            return View(repository.Products.OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).Take(PageSize));
        }
    }
}