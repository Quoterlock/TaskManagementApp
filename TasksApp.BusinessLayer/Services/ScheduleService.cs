using System.Globalization;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleSchemeRepository _scheduleRepository;
        private readonly IAdapterME<ScheduleBlockModel, ScheduleItemEntity> _adapter;
        private readonly IAdapterME<ScheduleTaskModel, ScheduleTaskEntity> _scheduleTaskAdapter;
        private readonly IScheduleTasksRepository _scheduleTasksRepository;

        public ScheduleService(IScheduleSchemeRepository scheduleRepository,
            IScheduleTasksRepository scheduleTasksRepository,
            IAdapterME<ScheduleBlockModel, ScheduleItemEntity> adapter,
            IAdapterME<ScheduleTaskModel, ScheduleTaskEntity> scheduleTaskAdapter)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleTasksRepository = scheduleTasksRepository;
            _adapter = adapter;
            _scheduleTaskAdapter = scheduleTaskAdapter;
        }

        public void AddBlock(ScheduleBlockModel block)
        {
            if(block != null)
            {
                try
                {
                    _scheduleRepository.Add(_adapter.ModelToEntity(block));
                } 
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void AddScheduleInTasks(DateOnly date)
        {
            var items = GetCurrentSchedule();
            foreach (var item in items)
            {
                foreach(var day in item.DaysOfWeek)
                {
                    if (day == date.DayOfWeek)
                    {
                        _scheduleTasksRepository.Add(new ScheduleTaskEntity
                        {
                            StartTime = date.ToDateTime(item.StartTime),
                            EndTime = date.ToDateTime(item.EndTime),
                            Text = item.Text,
                        });
                    }
                }
            }
        }

        public void AddScheduleForRestOfWeek(DateTime date)
        {
            AddScheduleInTasks(DateOnly.FromDateTime(date));
            do
            {
                date = date.AddDays(1);
                AddScheduleInTasks(DateOnly.FromDateTime(date));
            } 
            while (date.DayOfWeek != DayOfWeek.Sunday);
        }

        public void ClearFutureTasks(DateTime date)
        {
            var futureTasks = _scheduleTasksRepository.GetAll().Where(t=>t.StartTime >= date);
            if(futureTasks != null)
                foreach(var task in futureTasks)
                    _scheduleTasksRepository.Delete(task.Id);
        }

        public void ClearSchedule()
        {
            var blocks = GetCurrentSchedule();
            try
            {
                foreach (var block in blocks)
                    _scheduleRepository.Delete(block.Id);
                ClearFutureTasks(DateTime.Now.Date);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ScheduleBlockModel> GetCurrentSchedule()
        {
            var items = _scheduleRepository.GetAll();
            var result = new List<ScheduleBlockModel>();
            foreach(var item in items) 
            {
                result.Add(_adapter.EntityToModel(item));
            }
            return result;
            
        }

        public void RemoveBlock(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    _scheduleRepository.Delete(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateBlock(ScheduleBlockModel block)
        {
            if (block != null)
            {
                try
                {
                    _scheduleRepository.Update(_adapter.ModelToEntity(block));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<ScheduleTaskModel> GetTasksByDate(DateTime date)
        {
            var tasks = _scheduleTasksRepository.GetAll().Where(t=>t.StartTime.Date == date.Date);
            var models = new List<ScheduleTaskModel>();
            if(tasks != null)
                foreach(var task in tasks)
                    models.Add(_scheduleTaskAdapter.EntityToModel(task));
            return models;
        }

        public void RemoveTask(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    _scheduleTasksRepository.Delete(id);
                } 
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("schedule_task_id");
        }

        public void UpdateTask(ScheduleTaskModel task)
        {
            if(task != null)
            {
                if (!string.IsNullOrEmpty(task.Id))
                {
                    try
                    {
                        _scheduleTasksRepository.Update(_scheduleTaskAdapter.ModelToEntity(task));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else throw new ArgumentNullException("schedule_task_id");
            }
            else throw new ArgumentNullException("schedule_task");
        }

        public void UpdateFutureTasksToScheme(DateTime date)
        {
            ClearFutureTasks(date);
            AddScheduleForRestOfWeek(date);
        }

        public ScheduleTaskModel GetTaskById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = _scheduleTasksRepository.Get(id);
                if (entity != null)
                    return _scheduleTaskAdapter.EntityToModel(entity);
                else 
                    throw new Exception("schedule task not found with id: " + id);
            }
            else throw new ArgumentNullException("schedule_task_id");
        }
    }
}
