using System.ComponentModel;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface IProjectsService
    {
        void AddProject(ProjectInfoModel projectInfo);
        void DeleteProject(ProjectModel project);
        void DeleteProject(string id);
        void UpdateProject(ProjectModel project);
        ProjectModel GetProjectById(string id);
        List<ProjectInfoModel> GetProjectsList();
        void Archive(ProjectModel project);
        void Unarchive(ProjectModel project);
    }
}
