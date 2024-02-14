using System.Globalization;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Services
{
    public class TasksPresenter : ITasksPresenter
    {
        private readonly ITasksService _tasks;
        private readonly IProjectsService _projects;

        public TasksPresenter(ITasksService tasks, IProjectsService projects)
        {
            _projects = projects;
            _tasks = tasks;
        }

        public List<CategoryModel> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> GetByDate(DateTime date, bool archiveIncluded)
        {
            var tasks = _tasks.GetTasksByDate(date, archiveIncluded);
            return tasks;
        }

        public Dictionary<DateTime, List<TaskModel>> GetByMonth(int year, int monthNumber, bool archiveIncluded)
        {
            var monthTasks = new Dictionary<DateTime, List<TaskModel>>();
            for(int i = 1; i < DateTime.DaysInMonth(year, monthNumber)+1; i++)
            {
                var date = new DateTime(year, monthNumber, i);
                monthTasks.Add(date, GetByDate(date, archiveIncluded));
            }
            return monthTasks;
        }

        public Dictionary<DateTime, List<TaskModel>> GetByWeek(int year, int weekNumber, bool archiveIncluded)
        {
            var weekTasks = new Dictionary<DateTime, List<TaskModel>>();
            var mondayDate = GetMondayDate(year, weekNumber);
            for (int i = 0; i < 7; i++)
            {
                var date = mondayDate.AddDays(i);
                weekTasks.Add(date, GetByDate(date, archiveIncluded));
            }
            return weekTasks;
        }

        public List<TaskModel> GetOverdueTasks(DateTime date)
        {
            return _tasks.GetOverdueTasks(date);
        }

        public ProjectModel GetProjectWithTasks(string projectId)
        {
            var project = _projects.GetProjectById(projectId);
            project.Tasks = _tasks.GetTasksByProject(projectId);
            return project;
        }

        private DateTime GetMondayDate(int year, int weekNumber)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                weekNumber -= 1;
            }

            return firstMonday.AddDays(weekNumber * 7);
        }

    }
}
