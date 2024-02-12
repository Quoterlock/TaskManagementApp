using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class MockTasksRepository : ITasksRepository
    {
        public List<TaskEntity> tasks;
        public MockTasksRepository()
        {
            tasks = SeedTasks();
        }
        public void Add(TaskEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Text))
                throw new Exception("Task text is empty");
            tasks.Add(entity);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("task_id");
        }

        public IEnumerable<TaskEntity> GetByDate(DateTime date)
        {
            return tasks.Where(task => task.StartTime.Date.Equals(date.Date)) ?? new List<TaskEntity>().AsEnumerable();
        }

        public TaskEntity GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("task_id");
            return tasks.Where(task => task.Id == id).FirstOrDefault() ?? new TaskEntity();
        }

        public IEnumerable<TaskEntity> GetByProject(string projectId)
        {
            if (string.IsNullOrEmpty(projectId)) throw new ArgumentNullException(nameof(projectId));
            return tasks.Where(task => task.ProjectId == projectId) ?? new List<TaskEntity>().AsEnumerable();
        }

        public IEnumerable<TaskEntity> GetOverdue(DateTime date)
        {
            return tasks.Where(t => t.StartTime < date && !t.IsDone);
        }

        public void Update(string id, TaskEntity entity)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
        }

        private List<TaskEntity> SeedTasks()
        {
            return new List<TaskEntity> {
                new TaskEntity
                {
                    Id = "0",
                    ProjectId = "0",
                    Text = "Task1",
                    StartTime = DateTime.Now,
                    Priority = 0
                },
                new TaskEntity
                {
                    Id = "1",
                    ProjectId = "0",
                    Text = "Task2",
                    StartTime = DateTime.Parse("2023-12-01"),
                    Priority = 1,
                    IsDone = true
                },
                new TaskEntity
                {
                    Id = "2",
                    ProjectId = "1",
                    Text = "Task3",
                    StartTime = DateTime.Parse("2024-02-01"),
                    Priority = 2
                },
                new TaskEntity
                {
                    Id = "3",
                    ProjectId = "0",
                    Text="Test time block",
                    StartTime = DateTime.Parse("2024-02-13 12:00"),
                    Priority = 0,
                    EndTime = DateTime.Parse("2024-02-13 16:00")
                }
            };
        }
    }
}
