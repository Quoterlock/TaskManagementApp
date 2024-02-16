using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.DataAccess.Entities
{
    public class ScheduleTaskEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
