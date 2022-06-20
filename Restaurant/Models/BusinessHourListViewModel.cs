namespace Restaurant.Models
{
    public class BusinessHourListViewModel
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public int HourStart { get; set; }
        public int MinuteStart { get; set; }
        public int HourEnd { get; set; }
        public int MinuteEnd { get; set; }
    }
}
