using Restaurant.Domain;

namespace Restaurant.Database
{
    public interface IRestaurantDatabase
    {
        Meal Insert(Meal movie);
        Meal GetMeal(int id);
        IEnumerable<Meal> GetMeals();
        void Update(int id, Meal movie);
        void Delete(int id);
    }
}
