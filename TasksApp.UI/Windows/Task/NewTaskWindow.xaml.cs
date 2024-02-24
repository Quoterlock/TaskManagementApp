using System;
using System.Collections;
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

        public NewTaskWindow(ServicesContainer services, string projectId, DateTime date)
        {
            InitializeComponent();
            _services = services;
            _projectId = projectId;
            
            if (string.IsNullOrEmpty(projectId))
                LoadProjects();
            else
                projectsComboBox.Visibility = Visibility.Collapsed;

            // init combo-boxes
            var time = TimeOnly.MinValue;
            for (int i = 0; i < 24 * 60 / 5; i++)
            {
                if (i != 0)
                    time = time.AddMinutes(5);
                startTimeComboBox.Items.Add(time.ToString());
                endTimeComboBox.Items.Add(time.ToString());
            }
            startTimeComboBox.SelectedIndex = 0;
            endTimeComboBox.SelectedIndex = 0;

            if (date != DateTime.MinValue)
            {
                scheduledCheckBox.IsChecked = true;
                dueToDatePicker.SelectedDate = date.Date;
                if(TimeOnly.FromDateTime(date) != TimeOnly.MinValue)
                {
                    timeBlockedCheckBox.IsChecked = true;
                    startTimeComboBox.Items.Add(TimeOnly.FromDateTime(date).ToString());
                    startTimeComboBox.SelectedIndex = startTimeComboBox.Items.Count - 1;
                }
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
                    Project = new ProjectInfoModel() { Id = _projectId },
                };

                if (scheduledCheckBox.IsChecked ?? false)
                {
                    task.IsScheduled = true;
                    task.IsTimeBlocked = timeBlockedCheckBox.IsChecked ?? false;
                }
                else
                {
                    task.IsScheduled = false;
                    task.IsTimeBlocked = false;
                }

                if (task.IsScheduled)
                    task.DueTo = DateOnly.FromDateTime(dueToDatePicker.SelectedDate ?? DateTime.MinValue);
                else
                    task.DueTo = DateOnly.MinValue;

                if (task.IsTimeBlocked)
                {
                    task.StartTime = TimeOnly.Parse((string)startTimeComboBox.SelectedValue);
                    task.EndTime = TimeOnly.Parse((string)endTimeComboBox.SelectedValue);
                }
                else
                {
                    task.StartTime = TimeOnly.MinValue;
                    task.EndTime = TimeOnly.MinValue;
                }

                try
                {
                    var tasksService = _services.Get<ITasksService>();
                    tasksService.AddTask(task);
                    this.Close();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(_projectId))
            {
                MessageBox.Show("Select project first");
                return false;
            }

            if (scheduledCheckBox.IsChecked ?? false)
            {
                if (dueToDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Select Due-To date");
                    return false;
                }

                if (timeBlockedCheckBox.IsChecked ?? false)
                {
                    try
                    {
                        var time1 = TimeOnly.Parse((string)startTimeComboBox.SelectedValue);
                        var time2 = TimeOnly.Parse((string)endTimeComboBox.SelectedValue);
                        if (time1 > time2)
                        {
                            if(time2 != TimeOnly.MinValue)
                            {
                                MessageBox.Show("Start time can't be larger than end time!");
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Write correct time format (HH:MM)");
                        return false;
                    }
                }
            }
            return true;
        }

        private void timeBlockedCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (timeBlockedCheckBox.IsChecked ?? false)
            {
                startTimeComboBox.IsEnabled = true;
                endTimeComboBox.IsEnabled = true;
            }
            else
            {
                startTimeComboBox.IsEnabled = false;
                endTimeComboBox.IsEnabled = false;
            }
        }

        private void scheduledCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (scheduledCheckBox.IsChecked?? false)
            {
                dueToDatePicker.IsEnabled = true;
                timeBlockedCheckBox.IsEnabled = true;
            }
            else
            {
                dueToDatePicker.IsEnabled= false;
                timeBlockedCheckBox.IsChecked = false;
                timeBlockedCheckBox.IsEnabled = false;
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
