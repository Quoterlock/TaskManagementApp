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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;
using TasksApp.UI.Windows;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        ServicesContainer _services;

        public HomePage(ServicesContainer services)
        {
            _services = services;
            InitializeComponent();
            LoadTasks();
        }

        void LoadTasks()
        {
            todayTasksListBox.Items.Clear();
            overdueTasksListBox.Items.Clear();

            var service = _services.Get<ITasksPresenter>();
            var todayTasks = service.GetByDate(DateTime.Now, false);
            var overdueTasks = service.GetOverdueTasks(DateTime.Now);

            // load today's tasks
            foreach( var task in todayTasks)
            {
                var item = GetTaskListItem(task);
                todayTasksListBox.Items.Add(item);
            }

            if(overdueTasks.Count == 0)
            {
                overdueTasksRow.MaxHeight = 0;
                overdueTasksLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                overdueTasksRow.MaxHeight = int.MaxValue;
                foreach (var task in overdueTasks)
                {
                    var item = GetTaskListItem(task);
                    overdueTasksListBox.Items.Add(item);
                }
            }
        }

        private ListBoxItem GetTaskListItem(TaskModel task)
        {
            var item = new ListBoxItem();
            item.Uid = task.Id;
            item.MouseDoubleClick += TaskSelectedEvent;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            var statusBtn = new Button();
            statusBtn.Uid = task.Id;


            var label = new Label();

            // if is overdue
            if(task.DueTo < DateOnly.FromDateTime(DateTime.Now))
            {
                label.Content = task.Text + " - "
                + task.Project.Name + " (" + task.DueTo.ToString() + ")";
            }
            else
            {
                label.Content = task.Text + " - "
                + task.Project.Name + " ("
                + task.StartTime.ToString() + "-"
                + task.EndTime.ToString() + ")";
            }


            var staticRes = new StaticResourceExtension();
            if (task.IsDone)
            {
                var icon = ResourceManager.GetIcon("checkedIcon");
                icon.Height = 20;
                icon.Width = 20;
                statusBtn.Content = icon;
                statusBtn.Click += MarkTaskUndoneEvent;
            }
            else
            {
                var icon = ResourceManager.GetIcon("uncheckedIcon");
                icon.Height = 20;
                icon.Width = 20;
                statusBtn.Content = icon;
                statusBtn.Click += MarkTaskDoneEvent;
            }

            stack.Children.Add(statusBtn);
            stack.Children.Add(label);
            item.Content = stack;
            return item;
        }

        private void TaskSelectedEvent(object sender, RoutedEventArgs e)
        {
            var id = ((ListBoxItem)sender).Uid;
            var window = new TaskDetailsWindow(id, _services);
            window.ShowDialog();
           
        }

        private void MarkTaskUndoneEvent(object sender, RoutedEventArgs e)
        {
            var taskId = ((Button)sender).Uid;
            ChangeTaskStatus(taskId, false);
            LoadTasks();
        }

        private void MarkTaskDoneEvent(object sender, RoutedEventArgs e)
        {
            var taskId = ((Button)sender).Uid;
            ChangeTaskStatus(taskId, true);
            LoadTasks();
        }

        private void ChangeTaskStatus(string taskId, bool status)
        {
            var service = _services.Get<ITasksService>();
            var task = service.GetTaskById(taskId);
            task.IsDone = status;
            service.UpdateTask(task);
        }
    }
}
