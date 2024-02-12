using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface IProjectsRepository
    {
        void Add(ProjectEntity entity);
        void Delete(string id);
        void Update(string id, ProjectEntity entity);
        ProjectEntity GetById(string id);
        IEnumerable<ProjectEntity> GetAll();
        IEnumerable<ProjectEntity> GetByMatch(Func<ProjectEntity, bool> predicate);
    }
}
