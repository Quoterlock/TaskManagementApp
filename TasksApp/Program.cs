using System.Globalization;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.BusinessLogic.Services;
using TasksApp.DataAccess.Interfaces;
using TasksApp.DataAccess.Repositories;

namespace TasksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ITasksRepository repo= new MockTasksRepository();
            var service = new TasksService(repo);
            IProjectsRepository projectsRepository= new MockProjectsRepository();
            var projectsService = new ProjectsService(projectsRepository);

            var presenter = new TasksPresenter(service, projectsService);
            var monthTasks = presenter.GetByMonth(DateTime.Now.Year, DateTime.Now.Month);
            var weekTasks = presenter.GetByWeek(DateTime.Now.Year, CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday));
            var todayTasks = presenter.GetByDate(DateTime.Now);
            var projectTasks = presenter.GetByProject(projectsService.GetProjectById("0"));
            var overdue = presenter.GetOverdueTasks(DateTime.Now);

            Console.WriteLine("Done");
        }
    }
}