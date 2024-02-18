namespace TasksApp.BusinessLogic.Models
{
    public class TaskModel : TaskBaseModel
    {
        public bool IsDone { get; set; }
        public bool IsTimeBlocked { get; set; }
        public bool IsScheduled { get; set; }
        public int Priority { get; set; }
        public ProjectInfoModel Project { get; set; } = new ProjectInfoModel();
    }
}
