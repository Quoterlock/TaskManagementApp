using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface ITasksRepository
    {
        void Add(TaskEntity entity);
        void Delete(string id);
        void Update(string id, TaskEntity entity);
        TaskEntity GetById(string id);
        IEnumerable<TaskEntity> GetByDate(DateTime date);
        IEnumerable<TaskEntity> GetByProject(string projectId);
        IEnumerable<TaskEntity> GetOverdue(DateTime date);
    }
}
