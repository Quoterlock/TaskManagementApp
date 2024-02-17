using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ScheduleTaskEntity> ScheduleTasks { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ScheduleItemEntity> ScheduleItems { get; set; }

        private string _connectionString;
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString, option => {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
