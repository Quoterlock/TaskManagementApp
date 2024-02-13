namespace TasksApp.DataAccess.Entities
{
    public class ProjectEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
    }
}
