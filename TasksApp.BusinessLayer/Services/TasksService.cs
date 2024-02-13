using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasks;

        public TasksService(ITasksRepository tasks) 
        {
            _tasks = tasks;
        }

        public void AddTask(TaskModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.Text))
                {
                    try
                    {
                        model.Id = Guid.NewGuid().ToString();
                        _tasks.Add(Convert(model));
                    } catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else throw new ArgumentNullException("Task_Model");
        }

        public void DeleteTask(TaskModel model)
        {
            if(model != null)
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    _tasks.Delete(model.Id);
                }
            }
            else throw new ArgumentNullException("Task_Model");
        }

        public TaskModel GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = _tasks.GetById(id);
                if (entity != null)
                    return Convert(entity);
                else 
                    throw new Exception("Task not found with id:" + id);
            }
            else throw new ArgumentNullException(nameof(id));
        }

        public List<TaskModel> GetTasksByDate(DateTime date)
        {
            var entities = _tasks.GetByDate(date);
            return ConvertToModels(entities);
        }

        public List<TaskModel> GetTasksByProject(string projectId)
        {
            if (!string.IsNullOrEmpty(projectId))
            {
                var entities = _tasks.GetByProject(projectId);
                return ConvertToModels(entities);
            }
            else throw new ArgumentNullException(nameof(projectId));
        }

        public void UpdateTask(TaskModel model)
        {
            if(model != null)
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    _tasks.Update(model.Id, Convert(model));
                } 
                else throw new ArgumentNullException(nameof(model.Id));
            } 
            else throw new ArgumentNullException(nameof(model));
        }

        public List<TaskModel> GetOverdueTasks(DateTime date)
        {
            var tasks = _tasks.GetOverdue(date);
            return ConvertToModels(tasks);
        }

        private static TaskModel Convert(TaskEntity entity)
        {
            return new TaskModel
            {
                Id = entity.Id,
                Text = entity.Text,
                DueTo = DateOnly.FromDateTime(entity.StartTime),
                StartTime = TimeOnly.FromDateTime(entity.StartTime),
                EndTime = TimeOnly.FromDateTime(entity.EndTime),
                Priority = entity.Priority,
                IsDone = entity.IsDone,
                Project = new ProjectModel() { Id = entity.ProjectId },
            };
        }

        private static List<TaskModel> ConvertToModels(IEnumerable<TaskEntity> entities)
        {
            var models = new List<TaskModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }

        private static TaskEntity Convert(TaskModel model)
        {
            return new TaskEntity
            {
                Id = model.Id,
                Text = model.Text,
                Priority = model.Priority,
                ProjectId = model.Project.Id,
                IsDone = model.IsDone,
                StartTime = model.DueTo.ToDateTime(model.StartTime),
                EndTime = model.DueTo.ToDateTime(model.EndTime)
            };
        }
    }
}
