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
            project.CategoryId = string.Empty;

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

        public void Add(ProjectEntity project)
        {
            ArgumentNullException.ThrowIfNull(project);

            if (!string.IsNullOrEmpty(project.Name))
            {
                if (!string.IsNullOrEmpty(project.CategoryId))
                {
                    if(_context.Categories.Any(c=>c.Id == project.CategoryId))
                    {
                        project.Id = Guid.NewGuid().ToString();
                        _context.Add(project);
                        _context.SaveChanges();
                    }
                }
                else throw new ArgumentNullException("project_category_id");
            }
            else throw new ArgumentNullException("project_name");
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var project = _context.Projects.FirstOrDefault(p => p.Id == id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                    var tasks = _context.Tasks.Where(t => t.ProjectId == project.Id);
                    _context.Tasks.RemoveRange(tasks);
                    _context.SaveChanges();
                }
                else throw new Exception("Project not found with id: " + id);
            }
            else throw new ArgumentNullException("project_id");
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return _context.Projects ?? Enumerable.Empty<ProjectEntity>();
        }

        public ProjectEntity Get(string id)
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
                _context.ChangeTracker.Clear();
                _context.Projects.Update(project);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(project));
        }
    }
}
