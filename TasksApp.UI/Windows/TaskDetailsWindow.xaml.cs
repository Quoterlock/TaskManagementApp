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
            taskTextBox.Text = _task.Text;
            startTimeTextBox.Text = _task.StartTime.ToString();
            endTimeTextBox.Text = _task.EndTime.ToString();
            dueToTextBox.Text = _task.DueTo.ToString();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            // save

            // hide btn
            SetChanged(false);
        }

        private void dueToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
    }
}
