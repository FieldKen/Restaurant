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

        [HttpGet]
        public IActionResult Detail([FromRoute] int id)
        {
            var meal = restaurantDatabase.GetMeal(id);

            var vm = new MealDetailViewModel
            {
                Description = meal.Description,
                Name = meal.Name,
                Price = meal.Price
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var meal = restaurantDatabase.GetMeal(id);

            var vm = new MealEditViewModel
            {
                Description = meal.Description,
                Name = meal.Name,
                Price = meal.Price
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] MealEditViewModel vm)
        {
            if (TryValidateModel(vm))
            {
                var meal = new Meal
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price
                };

                restaurantDatabase.Update(id, meal);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var meal = restaurantDatabase.GetMeal(id);

            var vm = new MealDeleteViewModel
            {
                Id = meal.Id,
                Name = meal.Name
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            restaurantDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
