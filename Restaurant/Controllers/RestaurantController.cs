using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Domain;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantDatabase restaurantDatabase;
        private IWebHostEnvironment webHostEnvironment;

        public RestaurantController(IRestaurantDatabase restaurantDatabase, IWebHostEnvironment webHostEnvironment)
        {
            this.restaurantDatabase = restaurantDatabase;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Summary()
        {
            var meals = restaurantDatabase.GetMeals().Select(x => new MealListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });

            var businessHours = restaurantDatabase.GetBusinessHours().Select(x => new BusinessHourListViewModel
            {
                Day = x.Day,
                HourStart = x.HourStart,
                MinuteStart = x.MinuteStart,
                HourEnd = x.HourEnd,
                MinuteEnd = x.MinuteEnd
            });

            var vm = new SummaryViewModel
            {
                Meals = meals,
                BusinessHours = businessHours
            };

            return View(vm);
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

                if (vm.Photo != null)
                {
                    string uniqueFileName = UploadPhoto(vm.Photo);
                    meal.PhotoUrl = Path.Combine("/photos", uniqueFileName);
                }

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
                Price = meal.Price,
                PhotoUrl = meal.PhotoUrl
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
                Price = meal.Price,
                PhotoUrl = meal.PhotoUrl
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

                var mealFromDb = restaurantDatabase.GetMeal(id);

                if (vm.Photo != null)
                {
                    if (!string.IsNullOrEmpty(mealFromDb.PhotoUrl))
                    {
                        DeletePhoto(mealFromDb.PhotoUrl);
                    }

                    string uniqueFileName = UploadPhoto(vm.Photo);
                    meal.PhotoUrl = Path.Combine("/photos", uniqueFileName);
                }
                else
                {
                    meal.PhotoUrl = mealFromDb.PhotoUrl;
                }


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

        private string UploadPhoto(IFormFile photo)
        {
            //uniqueFileName = 0123456789.jpg
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

            //pathName = www.mijncoolewebsite.be/photos/
            string pathName = Path.Combine(webHostEnvironment.WebRootPath, "photos");

            //fileNameWithPath = www.mijncoolewebsite.be/photos/0123456789.jpg
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return uniqueFileName;
        }

        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(webHostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
        }
    }
}
