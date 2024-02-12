using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface ITasksPresenter
    {
        List<TaskModel> GetAllUndoneTasks();
        Dictionary<TaskStatusEnum, List<TaskModel>> GetByProject(ProjectModel project);
        Dictionary<DateTime, List<TaskModel>> GetByWeek(int year, int weekNumber);
        List<TaskModel> GetByDate(DateTime date);
        Dictionary<DateTime, List<TaskModel>> GetByMonth(int year, int monthNumber);
        List<TaskModel> SortByPriority();
        List<TaskModel> GetOverdueTasks(DateTime date);
    }
}
