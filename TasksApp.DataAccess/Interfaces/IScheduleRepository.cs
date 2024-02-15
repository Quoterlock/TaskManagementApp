using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface IScheduleRepository
    {
        void AddItem(ScheduleItemEntity item);
        void DeleteItem(string id);
        void UpdateItem(ScheduleItemEntity item);
        IEnumerable<ScheduleItemEntity> GetItems();
    }
}
