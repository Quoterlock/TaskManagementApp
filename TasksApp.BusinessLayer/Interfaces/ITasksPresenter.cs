using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface ITasksPresenter
    {
        ProjectModel GetProjectWithTasks(string projectId);

        List<CategoryModel> GetAllCategories();

        Dictionary<DateTime, List<TaskModel>> GetByWeek(int year, int weekNumber, bool archiveIncluded);
        
        List<TaskModel> GetByDate(DateTime date, bool archiveIncluded);
        
        Dictionary<DateTime, List<TaskModel>> GetByMonth(int year, int monthNumber, bool archiveIncluded);
                
        List<TaskModel> GetOverdueTasks(DateTime date);
    }
}
