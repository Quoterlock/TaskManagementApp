using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasks;
        private readonly IAdapterME<TaskModel, TaskEntity> _tasksAdapter;
        private readonly IProjectsService _projectsService;

        public TasksService(ITasksRepository tasks, IProjectsService projectsService, IAdapterME<TaskModel, TaskEntity> tasksAdapter) 
        {
            _tasks = tasks;
            _tasksAdapter = tasksAdapter;
            _projectsService = projectsService;
        }

        public void AddTask(TaskModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.Text))
                {
                    try
                    {
                        // TODO: add check for category existence
                        _tasks.Create(_tasksAdapter.ModelToEntity(model));
                    } 
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else throw new ArgumentNullException("task_model");
        }

        public void DeleteTask(TaskModel model)
        {
            if(model != null)
            {
                try
                {
                    _tasks.Delete(_tasksAdapter.ModelToEntity(model));
                }
                catch (Exception ex) 
                { 
                    throw new Exception(ex.Message);
                }

            }
            else throw new ArgumentNullException("task_model");
        }

        public void UpdateTask(TaskModel model)
        {
            if (model != null)
            {
                try
                {
                    _tasks.Update(_tasksAdapter.ModelToEntity(model));
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException(nameof(model));
        }

        public TaskModel GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = _tasks.GetById(id);
                if (entity != null)
                {
                    var task = _tasksAdapter.EntityToModel(entity);
                    task.Project = _projectsService.GetProjectsList()
                        .FirstOrDefault(p => p.Id == entity.ProjectId) ?? new ProjectInfoModel();
                    return task;
                }
                else throw new Exception("Task not found with id:" + id);
            }
            else throw new ArgumentNullException(nameof(id));
        }

        public List<TaskModel> GetTasksByDate(DateTime date, bool archiveIncluded)
        {
            if (archiveIncluded)
                return ConvertToModels(_tasks.GetByMatch(t => 
                    t.StartTime.Date.Equals(date.Date)));
            else
                return ConvertToModels(_tasks.GetByMatch(t => 
                    t.StartTime.Date.Equals(date.Date) && 
                    t.IsArchived == false));
        }

        public List<TaskModel> GetTasksByProject(string projectId)
        {
            if (!string.IsNullOrEmpty(projectId))
            {
                var entities = _tasks.GetByMatch(t=>t.ProjectId == projectId);
                return ConvertToModels(entities);
            }
            else throw new ArgumentNullException(nameof(projectId));
        }

        public List<TaskModel> GetOverdueTasks(DateTime date)
        {
            var tasks = _tasks.GetByMatch(t=>t.StartTime.Date < date.Date);
            return ConvertToModels(tasks);
        }

        private List<TaskModel> ConvertToModels(IEnumerable<TaskEntity> entities)
        {
            var projects = _projectsService.GetProjectsList();
            var models = new List<TaskModel>();
            foreach (var entity in entities)
            {
                var model = _tasksAdapter.EntityToModel(entity);
                var project =
                model.Project = projects.FirstOrDefault(p => p.Id == entity.ProjectId) ?? new ProjectInfoModel();
                models.Add(model);
            }
            return models;
        }
    }
}
