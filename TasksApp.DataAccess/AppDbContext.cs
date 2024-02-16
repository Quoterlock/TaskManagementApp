using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var connectionString = @"Data Source=" + myDocumentsPath + "\\TasksApp\\Tasks.db";

            optionsBuilder.UseSqlite(connectionString, option => {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
