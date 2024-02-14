using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Models;

namespace TasksApp.UI.ViewModels
{
    public class ProjectViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
        public List<TaskModel> UndoneTasks { get; set; } = [];
        public List<TaskModel> DoneTasks { get; set; } = [];
    }
}
