using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        public ProjectsService(IProjectsRepository projectsRepository) 
        {
            _projectsRepository = projectsRepository;
        }
        public void AddProject(ProjectModel project)
        {
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.Name))
                {
                    project.Id = Guid.NewGuid().ToString();
                    _projectsRepository.Add(Convert(project));
                }
                else throw new ArgumentNullException(nameof(project.Name));
            }
            else throw new ArgumentNullException(nameof(project));
        }

        public void DeleteProject(ProjectModel project)
        {
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.Id))
                {
                    _projectsRepository.Delete(project.Id);
                }
                else throw new ArgumentNullException(nameof(project.Id));
            }
            else throw new ArgumentNullException(nameof(project));
        }

        public List<ProjectModel> GetAll()
        {
            return ConvertToModels(_projectsRepository.GetAll());
        }

        public List<ProjectModel> GetAllArchived()
        {
            return ConvertToModels(_projectsRepository.GetByMatch(p => p.IsArchived == true));
        }

        public Dictionary<string, List<ProjectModel>> GetAllGrouped(bool isArchived)
        {
            var projects = isArchived ? GetAllArchived() : GetAllNotArchived();
            var grouped = new Dictionary<string, List<ProjectModel>>();
            foreach(var project in projects)
            {
                if (!grouped.ContainsKey(project.Category))
                    grouped[project.Category] = new List<ProjectModel>();
                grouped[project.Category].Add(project);
            }
            return grouped;
        }

        public List<ProjectModel> GetAllNotArchived()
        {
            return ConvertToModels(_projectsRepository.GetByMatch(p => p.IsArchived == false));
        }

        public ProjectModel GetProjectById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = _projectsRepository.GetById(id);
                if (entity != null)
                    return Convert(entity);
                else 
                    throw new Exception("Project not found with id:" + id);
            }
            else throw new ArgumentNullException("project_id");
        }

        public void UpdateProject(ProjectModel project)
        {
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.Id))
                {
                    if (!string.IsNullOrEmpty(project.Name))
                    {
                        _projectsRepository.Update(project.Id, Convert(project));
                    }
                    else throw new Exception("New project name is null or empty");
                }
                else throw new ArgumentNullException(nameof(project.Id));
            }
            else throw new ArgumentNullException(nameof(project));
        }

        private ProjectEntity Convert(ProjectModel model)
        {
            return new ProjectEntity
            {
                Id = model.Id,
                Name = model.Name,
                IsArchived = model.IsArchived,
                Category = model.Category
            };
        }
        private ProjectModel Convert(ProjectEntity entity)
        {
            return new ProjectModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsArchived = entity.IsArchived,
                Category = entity.Category
            };
        }
        private List<ProjectModel> ConvertToModels(IEnumerable<ProjectEntity> entities)
        {
            var models = new List<ProjectModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }
    }
}
