using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.BusinessLogic.Models
{
    public class ProjectModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public string Category { get; set; } = string.Empty;
    }
}