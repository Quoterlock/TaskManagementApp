using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _context;

        public TasksRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Add(TaskEntity task)
        {
            if(task != null)
            {
                task.Id = Guid.NewGuid().ToString();
                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    _context.SaveChanges();
                } else throw new Exception("Task not found with id: " + id);
            }
            else throw new ArgumentNullException("task_id");
        }

        public TaskEntity Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                    return task;
                else
                    throw new Exception("Task not found with id: " + id);
            }
            else throw new ArgumentNullException("task_id");
        }

        public void Update(TaskEntity task)
        {
            if (task != null)
            {
                _context.ChangeTracker.Clear();
                _context.Update(task);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(task));
        }

        public IEnumerable<TaskEntity> GetByMatch(Func<TaskEntity, bool> predicate)
        {
            return _context.Tasks.Where(predicate) ?? Enumerable.Empty<TaskEntity>();
        }

        public IEnumerable<TaskEntity> GetAll()
        {
            return _context.Tasks ?? Enumerable.Empty<TaskEntity>();
        }
    }
}
