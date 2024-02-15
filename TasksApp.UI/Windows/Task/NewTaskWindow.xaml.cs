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
        private ServicesContainer _services;
        private string _projectId;
        private List<ProjectInfoModel> _projects = [];
        public NewTaskWindow(ServicesContainer services, string projectId)
        {
            InitializeComponent();
            _services = services;
            _projectId = projectId;
            
            if (string.IsNullOrEmpty(projectId))
            {
                LoadProjects();
                if(_projects.Count == 0)
                {
                    MessageBox.Show("Create project first!");
                    this.Close();
                } 
            } 
            else
            {
                projectsComboBox.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadProjects()
        {
            _projects = _services.Get<IProjectsService>()
                .GetProjectsList()
                .Where(p=>p.IsArchived == false)
                .ToList() 
                ?? [];
            foreach (var project in _projects)
            {
                var item = new ComboBoxItem();
                item.Content = project.Name;
                item.Uid = project.Id;
                item.Selected += ProjectSelectedEvent;
                projectsComboBox.Items.Add(item);
            }
        }

        private void ProjectSelectedEvent(object sender, RoutedEventArgs e)
        {
            _projectId = ((ComboBoxItem)sender).Uid;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                var task = new TaskModel()
                {
                    Text = taskTextBox.Text,
                    DueTo = DateOnly.Parse(dueToTextBox.Text),
                    StartTime = TimeOnly.Parse(startTimeTextBox.Text),
                    EndTime = TimeOnly.Parse(endTimeTextBox.Text),
                    Project = new ProjectInfoModel() { Id = _projectId }
                };
                var tasksService = _services.Get<ITasksService>();
                tasksService.AddTask(task);
                this.Close();
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(_projectId))
            {
                MessageBox.Show("Select project first");
                return false;
            }

            try
            {
                var time = TimeOnly.Parse(startTimeTextBox.Text);
                var time2 = TimeOnly.Parse(endTimeTextBox.Text);
                if(time > time2) 
                { 
                    MessageBox.Show("Start time must be before end time"); 
                    return false; 
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Write correct time format");
                return false;
            }
            try
            {
                var date = DateOnly.Parse(dueToTextBox.Text);
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Write correct date format");
                return false;
            }

            return true;
            
        }

        private void projectsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
