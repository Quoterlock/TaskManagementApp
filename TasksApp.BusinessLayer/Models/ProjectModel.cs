namespace TasksApp.BusinessLogic.Models
{
    public class ProjectModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
        public CategoryModel Category { get; set; } = new CategoryModel();
        public List<TaskModel> Tasks { get; set; } = [];
        public bool IsArchived { get; set; } = false;
        public string ColorHex { get; set; } = "FFFFFF";
    }
    public class ProjectInfoModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public string ColorHex { get; set; } = "FFFFFF";
    }
}