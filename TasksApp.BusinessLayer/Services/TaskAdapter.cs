using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;

namespace TasksApp.BusinessLogic.Services
{
    public class TaskAdapter : IAdapterME<TaskModel, TaskEntity>
    {
        public TaskModel EntityToModel(TaskEntity entity)
        {
            return new TaskModel
            {
                Id = entity.Id,
                StartTime = TimeOnly.FromDateTime(entity.StartTime),
                EndTime = TimeOnly.FromDateTime(entity.EndTime),
                DueTo = DateOnly.FromDateTime(entity.StartTime),
                IsDone = entity.IsDone,
                Text = entity.Text,
                Priority = entity.Priority,
            };
        }

        public TaskEntity ModelToEntity(TaskModel model)
        {
            return new TaskEntity
            {
                Id = model.Id,
                Priority = model.Priority,
                StartTime = model.DueTo.ToDateTime(model.StartTime),
                EndTime = model.DueTo.ToDateTime(model.EndTime),
                Text = model.Text,
                IsArchived = model.Project.IsArchived,
                IsDone = model.IsDone,
                ProjectId = model.Project.Id
            };
        }
    }
}
