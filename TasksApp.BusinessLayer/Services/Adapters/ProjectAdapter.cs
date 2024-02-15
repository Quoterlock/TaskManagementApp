using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;

namespace TasksApp.BusinessLogic.Services.Adapters
{
    public class ProjectAdapter : IAdapterME<ProjectModel, ProjectEntity>
    {
        public ProjectModel EntityToModel(ProjectEntity entity)
        {
            return new ProjectModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsArchived = entity.
                IsArchived,
                NoteText = entity.NoteText
            };
        }

        public ProjectEntity ModelToEntity(ProjectModel model)
        {
            return new ProjectEntity
            {
                Id = model.Id,
                Name = model.Name,
                IsArchived = model.IsArchived,
                NoteText = model.NoteText,
                CategoryId = model.Category.Id
            };
        }
    }
}
