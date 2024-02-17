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
    /// Interaction logic for TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        private bool isChanged;
        private ServicesContainer _services;
        private TaskModel _task;
        private string selectedProjectId = string.Empty;
        public bool IsModified = false;

        public TaskDetailsWindow(string taskId, ServicesContainer services)
        {
            _services = services;
            _task = new TaskModel() { Id = taskId };
            InitializeComponent();
            LoadTask();
            isChanged = false;
            saveBtn.Visibility = Visibility.Collapsed;
        }

        private void LoadTask()
        {
            _task = _services.Get<ITasksService>().GetTaskById(_task.Id);

            // fill fields
            MarkDoneChanged();

            if (_task.StartTime == _task.EndTime && _task.StartTime == TimeOnly.MinValue) 
            { 
                TimeBlockedStatus(false); 
            }
            else
            {
                timeBlockedCheckBox.IsChecked = true;
            }

            taskTextBox.Text = _task.Text;
            startTimeTextBox.Text = _task.StartTime.ToString();
            endTimeTextBox.Text = _task.EndTime.ToString();
            dueToTextBox.Text = _task.DueTo.ToString();
            selectedProjectId = _task.Project.Id;

            // load projects to select from
            LoadProjectsList();
        }

        private void LoadProjectsList()
        {
            var projects = _services.Get<IProjectsService>().GetProjectsList();
            foreach (var project in projects)
            {
                var item = new ComboBoxItem();
                item.Content = project.Name;
                item.Uid = project.Id;
                item.Selected += ProjectSelectedEvent;

                projectsComboBox.Items.Add(item);

                if (project.Id.Equals(selectedProjectId))
                    projectsComboBox.SelectedItem = item;
            }
        }

        private void ProjectSelectedEvent(object sender, RoutedEventArgs e)
        {
            selectedProjectId = ((ComboBoxItem)sender).Uid;
            SetChanged(true);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            // save
            _task.Text = taskTextBox.Text;
            _task.StartTime = TimeOnly.Parse(startTimeTextBox.Text);
            _task.EndTime = TimeOnly.Parse(endTimeTextBox.Text);
            _task.DueTo = DateOnly.Parse(dueToTextBox.Text);
            //_task.Priority = 
            _task.Project = _services.Get<IProjectsService>()
                .GetProjectsList()
                .FirstOrDefault(p => p.Id == selectedProjectId) 
                ?? _task.Project;

            try
            {
                _services.Get<ITasksService>().UpdateTask(_task);
                // hide btn
                SetChanged(false);
                IsModified = true;
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isChanged)
                SetChanged(true);
        }

        public void SetChanged(bool value)
        {
            isChanged = value;

            if(value)
                saveBtn.Visibility = Visibility.Visible;
            else 
                saveBtn.Visibility = Visibility.Collapsed;
        }

        public void MarkDoneChanged()
        {
            if (_task.IsDone)
                markDoneBtn.Content = ResourceManager.GetIcon("checkedIcon");
            else
                markDoneBtn.Content = ResourceManager.GetIcon("uncheckedIcon");
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this task?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    _services.Get<ITasksService>().DeleteTask(_task);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void markDoneBtn_Click(object sender, RoutedEventArgs e)
        {
            _task.IsDone = !_task.IsDone;
            MarkDoneChanged();
            SetChanged(true);
        }

        private void timeBlockedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TimeBlockedStatus(timeBlockedCheckBox.IsChecked ?? false);
        }

        void TimeBlockedStatus(bool value)
        {
            if (!value)
            {
                endTimeTextBox.Text = TimeOnly.MinValue.ToString();
                startTimeTextBox.Text = TimeOnly.MinValue.ToString();

                startTimeTextBox.IsEnabled = false;
                endTimeTextBox.IsEnabled = false;
            }
            else
            {
                endTimeTextBox.Text = _task.EndTime.ToString();
                startTimeTextBox.Text = _task.StartTime.ToString();
                startTimeTextBox.IsEnabled = true;
                endTimeTextBox.IsEnabled = true;
            }
        }
    }
}
