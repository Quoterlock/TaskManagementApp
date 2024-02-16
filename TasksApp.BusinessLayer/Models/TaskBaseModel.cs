namespace TasksApp.BusinessLogic.Models
{
    public class TaskBaseModel
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public DateOnly DueTo { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
