using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface ITasksRepository
    {
        void Create(TaskEntity task);
        void Delete(TaskEntity task);
        void Update(TaskEntity task);
        TaskEntity GetById(string id);
        IEnumerable<TaskEntity> GetByMatch(Func<TaskEntity, bool> predicate);
    }
}
