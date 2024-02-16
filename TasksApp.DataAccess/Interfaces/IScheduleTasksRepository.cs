using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface IScheduleTasksRepository
    {
        void Add(ScheduleTaskEntity task);
        void Update(ScheduleTaskEntity task);
        void Remove(string id);
        IEnumerable<ScheduleTaskEntity> GetAll();
        ScheduleTaskEntity GetById(string id);
    }
}
