using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface ITasksService
    {
        void AddTask(TaskModel model);
        void DeleteTask(TaskModel model);
        void UpdateTask(TaskModel model);
        TaskModel GetTaskById(string id);
        List<TaskModel> GetTasksByDate(DateTime date, bool archivedIncluded);
        List<TaskModel> GetTasksByProject(string projectId);
        List<TaskModel> GetOverdueTasks(DateTime date);
    }
}
