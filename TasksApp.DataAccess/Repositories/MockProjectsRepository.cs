using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class MockProjectsRepository : IProjectsRepository
    {
        public List<ProjectEntity> projects;
        public MockProjectsRepository() {
            projects = SeedData();
        }

        private static List<ProjectEntity> SeedData()
        {
            return new List<ProjectEntity>()
            {
                new ProjectEntity { Id = "0", Name = "Project1", IsArchived = false, Category = "Personal" },
                new ProjectEntity { Id = "1", Name = "Project2", IsArchived = false, Category = "Study" },
                new ProjectEntity { Id = "2", Name = "Project3", IsArchived = true, Category = "DevProjects" },
            };
        }

        public void Add(ProjectEntity entity)
        {
            if (entity != null)
                projects.Add(entity);
            else throw new ArgumentNullException(nameof(entity));
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
                projects.RemoveAll(p => p.Id == id);
            else 
                throw new ArgumentNullException("project_id");
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return projects;
        }

        public ProjectEntity GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("project_id");
            return projects.Where(p => p.Id == id).FirstOrDefault() ?? new ProjectEntity();
        }

        public IEnumerable<ProjectEntity> GetByMatch(Func<ProjectEntity, bool> predicate)
        {
            return projects.Where(predicate);
        }

        public void Update(string id, ProjectEntity entity)
        {
            if(string.IsNullOrEmpty(id)) throw new ArgumentNullException("project_id");
            if (entity == null) throw new ArgumentNullException("project_entity");
        }
    }
}
