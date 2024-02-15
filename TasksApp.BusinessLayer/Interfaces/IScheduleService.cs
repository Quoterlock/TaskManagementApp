using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface IScheduleService
    {
        List<ScheduleBlockModel> GetCurrentSchedule();
        void AddBlock(ScheduleBlockModel block);
        void RemoveBlock(string id);
        void UpdateBlock(ScheduleBlockModel block);
        void ClearSchedule();
    }
}
