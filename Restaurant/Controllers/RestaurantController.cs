using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Domain;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] MealCreateViewModel vm)
        {
            if (TryValidateModel(vm))
            {
                var meal = new Meal
                {
                    Description = vm.Description,
                    Name = vm.Name,
                    Price = vm.Price
                };

                restaurantDatabase.Insert(meal);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
