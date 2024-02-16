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

namespace TasksApp.UI.Windows.Schedule
{
    /// <summary>
    /// Interaction logic for ScheduleTaskDetailsWindow.xaml
    /// </summary>
    public partial class ScheduleTaskDetailsWindow : Window
    {
        private bool isChanged = false;
        private ServicesContainer _services;
        private ScheduleTaskModel _task;

        public bool IsModified { get; set; }

        public ScheduleTaskDetailsWindow(ServicesContainer services, string taskId)
        {
            _services = services;
            InitializeComponent();
            IsChangedState(false);
            IsModified = false;
            GetTask(taskId);
        }

        void GetTask(string id)
        {
            try
            {
                _task = _services.Get<IScheduleService>().GetTaskById(id);

                taskTextBox.Text = _task.Text;
                startTimeTextBox.Text = _task.StartTime.ToString();
                endTimeTextBox.Text = _task.EndTime.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                _task.StartTime = TimeOnly.Parse(startTimeTextBox.Text);
                _task.EndTime = TimeOnly.Parse(endTimeTextBox.Text);
                _task.Text = taskTextBox.Text;

                try
                {
                    _services.Get<IScheduleService>().UpdateTask(_task);
                    IsChangedState(false);
                    IsModified = true;
                } 
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool CheckInput()
        {
            try
            {
                var time1 = TimeOnly.Parse(startTimeTextBox.Text);
                var time2 = TimeOnly.Parse(endTimeTextBox.Text);
                if(time1 >= time2)
                {
                    MessageBox.Show("Start time can't be larger than end time!");
                    return false;
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Write correct time format (HH:MM)");
                return false;
            }
            return true;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this task?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    _services.Get<IScheduleService>().RemoveTask(_task.Id);
                    IsModified = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void IsChangedState(bool value)
        {
            isChanged = value;
            if (value)
                saveBtn.Visibility = Visibility.Visible;
            else 
                saveBtn.Visibility = Visibility.Collapsed;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            IsChangedState(true);
        }
    }
}
