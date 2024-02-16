using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;

namespace TasksApp.BusinessLogic.Services.Adapters
{
    public class ScheduleTaskAdapter : IAdapterME<ScheduleTaskModel, ScheduleTaskEntity>
    {
        public ScheduleTaskModel EntityToModel(ScheduleTaskEntity entity)
        {
            return new ScheduleTaskModel
            {
                Id = entity.Id,
                Text = entity.Text,
                DueTo = DateOnly.FromDateTime(entity.StartTime),
                StartTime = TimeOnly.FromDateTime(entity.StartTime),
                EndTime = TimeOnly.FromDateTime(entity.EndTime),
            };
        }

        public ScheduleTaskEntity ModelToEntity(ScheduleTaskModel model)
        {
            return new ScheduleTaskEntity
            {
                Id = model.Id,
                Text = model.Text,
                StartTime = model.DueTo.ToDateTime(model.StartTime),
                EndTime = model.DueTo.ToDateTime(model.EndTime),
            };
        }
    }
}
