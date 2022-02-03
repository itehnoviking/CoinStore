using CoinStore.Models;
using CoinStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoinStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository repository;
        public int PageSize = 3;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index (string category, int productPage = 1)
        {
            return View(
                new ProductsListViewModel
                {
                    Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category)
                        .Count()
                    },
                    CurrentCategory = category
                }); 
            
            
        }
    }
}