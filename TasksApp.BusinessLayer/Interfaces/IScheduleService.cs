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
        void AddScheduleInTasks(DateOnly date);
        List<ScheduleTaskModel> GetTasksByDate(DateTime date);
        ScheduleTaskModel GetTaskById(string id);
        void RemoveTask(string id);
        void UpdateTask(ScheduleTaskModel task);
        void AddScheduleForRestOfWeek(DateTime date);
        void ClearFutureTasks(DateTime date);
        void UpdateFutureTasksToScheme(DateTime date);
    }
}
