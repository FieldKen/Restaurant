namespace Restaurant.Models
{
    public class SummaryViewModel
    {
        public IEnumerable<MealListViewModel> Meals { get; set; }
        public IEnumerable<BusinessHourListViewModel> BusinessHours { get; set; }
    }
}
