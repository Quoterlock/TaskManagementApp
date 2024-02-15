using Microsoft.EntityFrameworkCore.Storage.Json;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IAdapterME<ProjectModel, ProjectEntity> _projectAdapter;
        public ProjectsService(
            IProjectsRepository projectsRepository, 
            IAdapterME<ProjectModel, ProjectEntity> projectAdapter) 
        {
            _projectsRepository = projectsRepository;
            _projectAdapter = projectAdapter;
        }
        
        public void AddProject(string name, string categoryId)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("project_name");
            if (string.IsNullOrEmpty(categoryId)) throw new ArgumentNullException("category_id");

            try
            {
                _projectsRepository.Create(name, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProject(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    // get project with no tasks
                    var entity = _projectsRepository.GetById(id);
                    _projectsRepository.Delete(entity);
                }
                catch (Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("project_id");
        }

        public void DeleteProject(ProjectModel project)
        {
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.Id))
                {
                    try
                    {
                        _projectsRepository.Delete(_projectAdapter.ModelToEntity(project));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else throw new ArgumentNullException(nameof(project.Id));
            }
            else throw new ArgumentNullException(nameof(project));
        }

        public List<ProjectModel> GetAll()
        {
            return ConvertToModels(_projectsRepository.GetAll());
        }

        public ProjectModel GetProjectById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = _projectsRepository.GetById(id);
                if (entity != null)
                {
                    var project = _projectAdapter.EntityToModel(entity);
                    project.Category = new CategoryModel { Id = entity.CategoryId };
                    return project;
                }
                else 
                    throw new Exception("Project not found with id:" + id);
            }
            else throw new ArgumentNullException("project_id");
        }

        public List<ProjectInfoModel> GetProjectsList()
        {
            var projects = _projectsRepository.GetAll();

            var resultList = new List<ProjectInfoModel>();
            foreach (var project in projects)
                resultList.Add(new ProjectInfoModel 
                { 
                    Id = project.Id, 
                    Name = project.Name, 
                    IsArchived = project.IsArchived,
                    CategoryId = project.CategoryId,
                });
            
            return resultList;
        }

        public void UpdateProject(ProjectModel project)
        {
            if (project == null) 
                throw new ArgumentNullException(nameof(project));
            if (string.IsNullOrEmpty(project.Id)) 
                throw new ArgumentNullException(nameof(project.Id));
            if (string.IsNullOrEmpty(project.Name)) 
                throw new Exception("New project name is null or empty");

            try
            {
                _projectsRepository.Update(_projectAdapter.ModelToEntity(project));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public void Archive(ProjectModel project)
        {
            if(project != null)
            {
                try
                {
                    _projectsRepository.Archive(_projectAdapter.ModelToEntity(project));
                }
                catch(Exception ex) 
                { 
                    throw new Exception(ex.Message); 
                }
            }
        }

        public void Unarchive(ProjectModel project)
        {
            if (project != null)
            {
                try
                {
                    _projectsRepository.Unarchive(_projectAdapter.ModelToEntity(project));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private List<ProjectModel> ConvertToModels(IEnumerable<ProjectEntity> entities)
        {
            var models = new List<ProjectModel>();
            foreach (var entity in entities)
                models.Add(_projectAdapter.EntityToModel(entity));
            return models;
        }
    }
}
