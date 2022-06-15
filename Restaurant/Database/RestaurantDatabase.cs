using Restaurant.Domain;

namespace Restaurant.Database
{
    public class RestaurantDatabase : IRestaurantDatabase
    {
        private int counter = 0;
        private List<Meal> meals;

        public RestaurantDatabase()
        {
            meals = new List<Meal>();

            Insert(new Meal
            {
                Name = "Scampi Diabolique",
                Description = "Pikante scampi's met pasta",
                Price = 15.30
            });

            Insert(new Meal
            {
                Name = "Vol-au-vent",
                Description = "Vol-au-vent met frietjes of aardappelen",
                Price = 12.50
            });

            Insert(new Meal
            {
                Name = "Spaghetti",
                Description = "Keuze uit bolognese of carbonara",
                Price = 13.50
            });

            Insert(new Meal
            {
                Name = "Soep",
                Description = "Keuze uit tomaten-, groente- of champignonsoep",
                Price = 8.20
            });
        }

        public Meal Insert(Meal meal)
        {
            meal.Id = ++counter;
            meals.Add(meal);
            return meal;
        }

        public Meal GetMeal(int id)
        {
            return meals.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Meal> GetMeals()
        {
            return meals;
        }

        public void Update(int id, Meal updatedMeal)
        {
            var meal = meals.FirstOrDefault(x => x.Id == id);
            if (meal != null)
            {
                meal.Price = updatedMeal.Price;
                meal.Name = updatedMeal.Name;
                meal.Description = updatedMeal.Description;
            }
        }

        public void Delete(int id)
        {
            var meal = meals.FirstOrDefault(x => x.Id == id);
            if (meal != null)
            {
                meals.Remove(meal);
            }
        }
    }
}
