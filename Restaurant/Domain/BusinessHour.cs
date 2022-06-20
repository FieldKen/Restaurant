namespace Restaurant.Domain
{
    public class BusinessHour
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public int HourStart { get; set; }
        public int MinuteStart { get; set; }
        public int HourEnd { get; set; }
        public int MinuteEnd { get; set; }

        public BusinessHour(DayOfWeek day, int hourStart, int minuteStart, int hourEnd, int minuteEnd)
        {
            Day = day;
            HourStart = hourStart;
            MinuteStart = minuteStart;
            HourEnd = hourEnd;
            MinuteEnd = minuteEnd;
        }
    }
}
