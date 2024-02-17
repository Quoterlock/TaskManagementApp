using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface IProjectsRepository
    {
        void Create(ProjectEntity project);
        void Delete(ProjectEntity project);
        void Update(ProjectEntity project);
        ProjectEntity GetById(string id);
        IEnumerable<ProjectEntity> GetAll();
        IEnumerable<ProjectEntity> GetByMatch(Func<ProjectEntity, bool> predicate);
        void Archive(ProjectEntity project);
        void Unarchive(ProjectEntity project);
    }
}
