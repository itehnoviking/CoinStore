using CoinStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(
                repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }

    }

}
