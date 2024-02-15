namespace TasksApp.DataAccess.Entities
{
    public class ScheduleItemEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; } = [];
    }
}
