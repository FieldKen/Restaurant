using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantDatabase restaurantDatabase;

        public RestaurantController(IRestaurantDatabase restaurantDatabase)
        {
            this.restaurantDatabase = restaurantDatabase;
        }

        public IActionResult Index()
        {
            var vm = restaurantDatabase.GetMeals().Select(x => new MealListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });

            return View(vm);
        }
    }
}
