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

            // init combo-boxes
            var time = TimeOnly.MinValue;
            for (int i = 0; i < 24*60/5; i++)
            {
                time = time.AddMinutes(5);
                startTimeComboBox.Items.Add(time.ToString());
                endTimeComboBox.Items.Add(time.ToString());
            }



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
            startTimeComboBox.SelectedValue = _task.StartTime.ToString();
            endTimeComboBox.SelectedValue = _task.EndTime.ToString();


            dueToDatePicker.SelectedDate = _task.DueTo.ToDateTime(TimeOnly.MinValue);
            selectedProjectId = _task.Project.Id;

            if (_task.IsScheduled)
            {
                scheduledCheckBox.IsChecked = true;
                if (_task.IsTimeBlocked)
                {
                    timeBlockedCheckBox.IsChecked = true;
                }
                else
                {
                    timeBlockedCheckBox.IsChecked = false;
                }
            }
            else
            {
                dueToDatePicker.SelectedDate = DateTime.Now;
                dueToDatePicker.IsEnabled = false;
                scheduledCheckBox.IsChecked = false;
            }


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
            if (CheckInput())
            {
                if(scheduledCheckBox.IsChecked ?? false)
                {
                    _task.IsScheduled = true;
                    _task.IsTimeBlocked = timeBlockedCheckBox.IsChecked ?? false;
                }
                else
                {
                    _task.IsScheduled = false;
                    _task.IsTimeBlocked = false;

                }


                if (_task.IsScheduled)
                    _task.DueTo = DateOnly.FromDateTime(dueToDatePicker.SelectedDate ?? DateTime.MinValue);
                else 
                    _task.DueTo = DateOnly.MinValue;

                if (_task.IsTimeBlocked)
                {
                    _task.StartTime = TimeOnly.Parse((string)startTimeComboBox.SelectedValue);
                    _task.EndTime = TimeOnly.Parse((string)endTimeComboBox.SelectedValue);
                }
                else
                {
                    _task.StartTime = TimeOnly.MinValue;
                    _task.EndTime = TimeOnly.MinValue;
                }

                // save
                _task.Text = taskTextBox.Text;

                //_task.Priority = 
                try
                {
                    _task.Project = _services.Get<IProjectsService>()
                        .GetProjectsList()
                        .FirstOrDefault(p => p.Id == selectedProjectId)
                        ?? _task.Project;


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
        }

        private bool CheckInput()
        {
            if(scheduledCheckBox.IsChecked ?? false)
            {
                if (dueToDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Select Due-To Date");
                    return false;
                }

                if(timeBlockedCheckBox.IsChecked ?? false)
                {
                    try
                    {
                        var time1 = TimeOnly.Parse((string)startTimeComboBox.SelectedValue);
                        var time2 = TimeOnly.Parse((string)endTimeComboBox.SelectedValue);
                        if (time1 > time2)
                        {
                            if (time2 != TimeOnly.MinValue)
                            {
                                MessageBox.Show("Start time can't be larger than end time!");
                                return false;
                            }
                        }
                    } 
                    catch(Exception ex) 
                    {
                        MessageBox.Show("Write correct time format (HH:MM)");
                        return false;
                    }
                }
            }
            return true;
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
            SetChanged(true);
        }

        void TimeBlockedStatus(bool value)
        {
            if (!value)
            {
                startTimeComboBox.IsEnabled = false;
                endTimeComboBox.IsEnabled = false;
            }
            else
            {
                startTimeComboBox.IsEnabled = true;
                endTimeComboBox.IsEnabled = true;
            }
        }

        private void scheduledCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (scheduledCheckBox.IsChecked ?? false)
            {
                dueToDatePicker.IsEnabled = true;
                timeBlockedCheckBox.IsEnabled = true;
            }
            else
            {
                dueToDatePicker.IsEnabled = false;
                timeBlockedCheckBox.IsChecked = false;
                timeBlockedCheckBox.IsEnabled = false;
            }
            SetChanged(true);
        }

        private void dueToDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetChanged(true);
        }

        private void endTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetChanged(true);
        }
    }
}
