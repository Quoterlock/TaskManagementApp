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
        void RemoveTask(string id);
        void UpdateTask(ScheduleTaskModel task);
        public void AddScheduleForRestOfWeek(DateTime date);
        public void ClearFutureTasks(DateTime date);
        public void UpdateFutureTasksToScheme(DateTime date);
    }
}
