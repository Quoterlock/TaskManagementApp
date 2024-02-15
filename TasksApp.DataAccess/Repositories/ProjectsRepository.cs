using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private AppDbContext _context;

        public ProjectsRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Archive(ProjectEntity project)
        {
            if (project == null) 
                throw new ArgumentNullException(nameof(project));
            if(string.IsNullOrEmpty(project.Id)) 
                throw new ArgumentNullException(nameof(project.Id));

            project.IsArchived = true;

            var tasks = _context.Tasks.Where(t=>t.ProjectId == project.Id);
            foreach (var task in tasks)
                task.IsArchived = true;

            _context.ChangeTracker.Clear();
            _context.Tasks.UpdateRange(tasks);
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void Unarchive(ProjectEntity project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            if (string.IsNullOrEmpty(project.Id))
                throw new ArgumentNullException(nameof(project.Id));

            project.IsArchived = false;

            var tasks = _context.Tasks.Where(t => t.ProjectId == project.Id);
            foreach (var task in tasks)
                task.IsArchived = false;

            _context.ChangeTracker.Clear();
            _context.Tasks.UpdateRange(tasks);
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void Create(string projectName, string categoryId)
        {
            if (!string.IsNullOrEmpty(projectName))
            {
                if (!string.IsNullOrEmpty(categoryId))
                {
                    if(_context.Categories.Any(c=>c.Id == categoryId))
                    {
                        _context.Add(new ProjectEntity 
                        { 
                            Id = Guid.NewGuid().ToString(), 
                            Name = projectName, 
                            CategoryId = categoryId 
                        });
                        _context.SaveChanges();
                    }
                }
                else throw new ArgumentNullException("project_category_id");
            }
            else throw new ArgumentNullException("project_name");
        }

        public void Delete(ProjectEntity project)
        {
            if (project != null)
            {
                _context.ChangeTracker.Clear();
                _context.Remove(project);
                var tasks = _context.Tasks.Where(t => t.ProjectId == project.Id);
                _context.Tasks.RemoveRange(tasks);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(project));
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return _context.Projects ?? Enumerable.Empty<ProjectEntity>();
        }

        public ProjectEntity GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var project = _context.Projects.FirstOrDefault(p => p.Id == id);
                if (project != null)
                    return project;
                else 
                    throw new Exception("Project not found with id: " + id);
            }
            else throw new ArgumentNullException("project_id");
        }

        public IEnumerable<ProjectEntity> GetByMatch(Func<ProjectEntity, bool> predicate)
        {
            return _context.Projects.Where(predicate) ?? Enumerable.Empty<ProjectEntity>();
        }

        public void Update(ProjectEntity project)
        {
            if (project != null)
            {
                _context.Projects.Update(project);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(project));
        }
    }
}
