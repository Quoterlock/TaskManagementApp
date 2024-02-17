using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class ScheduleTasksRepository : IScheduleTasksRepository
    {
        private readonly AppDbContext _context;

        public ScheduleTasksRepository(AppDbContext context)
        {
            _context = context;
        }
       
        public void Add(ScheduleTaskEntity task)
        {
            if (task != null)
            {
                task.Id = Guid.NewGuid().ToString();
                _context.ScheduleTasks.Add(task);
                _context.SaveChanges();
            }
        }

        public void Update(ScheduleTaskEntity task)
        {
            if (task != null)
            {
                _context.ChangeTracker.Clear();
                _context.ScheduleTasks.Update(task);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(task));
        }

        public IEnumerable<ScheduleTaskEntity> GetAll()
        {
            return _context.ScheduleTasks;
        }

        public ScheduleTaskEntity Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var task = _context.ScheduleTasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                    return task;
                else
                    throw new Exception("Task not found with id: " + id);
            }
            else throw new ArgumentNullException("task_id");
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var task = _context.ScheduleTasks.FirstOrDefault(p => p.Id == id);
                _context.ChangeTracker.Clear();
                if (task != null)
                {
                    _context.ScheduleTasks.Remove(task);
                    _context.SaveChanges();
                }
                else throw new Exception("schedule task doesn't exist with id: " + id);
            }
            else throw new Exception("schedule_task_id is null or empty string");
        }
    }
}
