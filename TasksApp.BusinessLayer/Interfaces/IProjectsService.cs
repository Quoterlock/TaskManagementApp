using System.ComponentModel;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface IProjectsService
    {
        void AddProject(ProjectModel project);
        void DeleteProject(ProjectModel project);
        void UpdateProject(ProjectModel project);
        ProjectModel GetProjectById(string id);
        List<ProjectModel> GetAll();
        List<ProjectModel> GetAllNotArchived();
        Dictionary<string, List<ProjectModel>> GetAllGrouped(bool isArchived);
        List<ProjectModel> GetAllArchived();
    }
}
