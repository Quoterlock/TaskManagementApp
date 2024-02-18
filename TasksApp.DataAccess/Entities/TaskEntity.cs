namespace TasksApp.DataAccess.Entities
{
    public class TaskEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDone { get; set; } = false;
        public int Priority { get; set; } = 0;
        public string ProjectId { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public bool IsTimeBlocked { get;set; } = false;
        public bool IsScheduled { get; set; } = false;
    }
}
