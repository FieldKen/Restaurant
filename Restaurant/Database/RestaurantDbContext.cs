using Microsoft.EntityFrameworkCore;
using Restaurant.Domain;

namespace Restaurant.Database
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<Meal> Meals { get; set; }
    }
}
