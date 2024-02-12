using System.Globalization;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Services
{
    public class TasksPresenter : ITasksPresenter
    {
        private ITasksService _tasks;
        private IProjectsService _projects;
        public TasksPresenter(ITasksService tasks, IProjectsService projects)
        {
            _projects = projects;
            _tasks = tasks;
        }
        public List<TaskModel> GetAllUndoneTasks()
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> GetByDate(DateTime date)
        {
            var tasks = _tasks.GetTasksByDate(date);
            return IncludeProjects(tasks);
        }

        public Dictionary<DateTime, List<TaskModel>> GetByMonth(int year, int monthNumber)
        {
            var monthTasks = new Dictionary<DateTime, List<TaskModel>>();
            for(int i = 1; i < DateTime.DaysInMonth(year, monthNumber)+1; i++)
            {
                var date = new DateTime(year, monthNumber, i);
                monthTasks.Add(date, IncludeProjects(GetByDate(date)));
            }
            return monthTasks;
        }

        public Dictionary<TaskStatusEnum, List<TaskModel>> GetByProject(ProjectModel project)
        {
            var tasks = _tasks.GetTasksByProject(project.Id);
            tasks = IncludeProjects(tasks);
            var sortedTasks = new Dictionary<TaskStatusEnum, List<TaskModel>>
            {
                { TaskStatusEnum.Done, tasks.Where(t => t.IsDone == true).ToList() },
                { TaskStatusEnum.Undone, tasks.Where(t => t.IsDone == false).ToList() }
            };
            return sortedTasks;
        }

        public Dictionary<DateTime, List<TaskModel>> GetByWeek(int year, int weekNumber)
        {
            var weekTasks = new Dictionary<DateTime, List<TaskModel>>();
            var mondayDate = GetMondayDate(year, weekNumber);
            for (int i = 0; i < 7; i++)
            {
                var date = mondayDate.AddDays(i);
                weekTasks.Add(date, IncludeProjects(GetByDate(date)));
            }
            return weekTasks;
        }

        public List<TaskModel> GetOverdueTasks(DateTime date)
        {
            return IncludeProjects(_tasks.GetOverdueTasks(date));
        }

        public List<TaskModel> SortByPriority()
        {
            throw new NotImplementedException();
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

        private List<TaskModel> IncludeProjects(List<TaskModel> tasks)
        {
            var tasksResult = new List<TaskModel>();
            foreach(var task in tasks) 
            {
                task.Project = _projects.GetProjectById(task.Project.Id);
                tasksResult.Add(task);
            }
            return tasksResult;
        }
    }
}
