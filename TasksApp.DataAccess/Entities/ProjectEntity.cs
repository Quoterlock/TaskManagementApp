using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.DataAccess.Entities
{
    public class ProjectEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsArchived { get; set; }
    }
}
