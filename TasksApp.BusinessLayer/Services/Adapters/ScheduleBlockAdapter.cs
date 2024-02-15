using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;

namespace TasksApp.BusinessLogic.Services.Adapters
{
    public class ScheduleBlockAdapter : IAdapterME<ScheduleBlockModel, ScheduleItemEntity>
    {
        public ScheduleBlockModel EntityToModel(ScheduleItemEntity entity)
        {
            return new ScheduleBlockModel
            {
                Id = entity.Id,
                Text = entity.Text,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                DaysOfWeek = entity.DaysOfWeek,
            };
        }

        public ScheduleItemEntity ModelToEntity(ScheduleBlockModel model)
        {
            return new ScheduleItemEntity
            {
                Id = model.Id,
                Text = model.Text,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                DaysOfWeek = model.DaysOfWeek,
            };
        }
    }
}
