using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAdapterME<ScheduleBlockModel, ScheduleItemEntity> _adapter;

        public ScheduleService(IScheduleRepository scheduleRepository, 
            IAdapterME<ScheduleBlockModel, ScheduleItemEntity> adapter)
        {
            _scheduleRepository = scheduleRepository;
            _adapter = adapter;
        }

        public void AddBlock(ScheduleBlockModel block)
        {
            if(block != null)
            {
                try
                {
                    _scheduleRepository.AddItem(_adapter.ModelToEntity(block));
                } 
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void ClearSchedule()
        {
            var blocks = GetCurrentSchedule();
            foreach (var block in blocks)
                _scheduleRepository.DeleteItem(block.Id);
        }

        public List<ScheduleBlockModel> GetCurrentSchedule()
        {
            var items = _scheduleRepository.GetItems();
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
                    _scheduleRepository.DeleteItem(id);
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
                    _scheduleRepository.UpdateItem(_adapter.ModelToEntity(block));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
