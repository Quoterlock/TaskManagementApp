using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface ITasksRepository : IRepository<TaskEntity>
    {
        IEnumerable<TaskEntity> GetByMatch(Func<TaskEntity, bool> predicate);
    }
}
