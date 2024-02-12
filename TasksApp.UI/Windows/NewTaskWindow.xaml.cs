using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        ServicesContainer _services;
        public NewTaskWindow(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel()
            {
                Text = taskTextBox.Text,
                DueTo = DateOnly.Parse(dueToTextBox.Text),
                StartTime = TimeOnly.Parse(startTimeTextBox.Text),
                EndTime = TimeOnly.Parse(endTimeTextBox.Text),
                Project = new ProjectModel() { Id = "0" }
            };
            var tasksService = _services.Get<ITasksService>();
            tasksService.AddTask(task);
            this.Close();
        }
    }
}
