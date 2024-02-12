using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.BusinessLogic.Models
{
    public class TaskModel
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateOnly DueTo { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public ProjectModel Project { get; set; } = new ProjectModel();
    }
}
