namespace TasksApp.BusinessLogic.Models
{
    public class CategoryModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<ProjectInfoModel> Projects { get; set; } = [];
    }
}
