using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface IProjectsRepository : IRepository<ProjectEntity>
    {
        IEnumerable<ProjectEntity> GetByMatch(Func<ProjectEntity, bool> predicate);
        void Archive(ProjectEntity project);
        void Unarchive(ProjectEntity project);
    }
}
