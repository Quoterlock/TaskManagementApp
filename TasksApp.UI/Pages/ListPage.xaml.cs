using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TasksApp.UI.Services;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.UI.Windows;
using TasksApp.BusinessLogic.Models;
using System.Net;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private readonly ServicesContainer _services;
        private string selectedCategoryId;
        private string selectedProjectId;

        public ListPage(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
            selectedProjectId = string.Empty;
            selectedCategoryId = string.Empty;
            LoadCategories();
        }

        private void LoadCategories()
        {
            categoriesListBox.Items.Clear();
            var service = _services.Get<ICategoriesService>();
            var categories = service.GetList();
            foreach (var category in categories)
            {
                var item = new ListBoxItem();
                item.Uid = category.Id;
                item.Selected += CategorySelectedEvent;

                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var label = new Label() { Content = category.Name };

                var editBtn = new Button() { Uid = category.Id };
                editBtn.Content = GetIcon("editIcon");
                editBtn.Click += EditCategoryEvent;

                var deleteBtn = new Button() { Uid = category.Id };
                deleteBtn.Content = GetIcon("deleteIcon");
                deleteBtn.Click += DeleteCategoryEvent;

                stack.Children.Add(label);
                stack.Children.Add(editBtn);
                stack.Children.Add(deleteBtn);

                item.Content = stack;
                categoriesListBox.Items.Add(item);
            }

            // add archive folder
            var archiveItem = new ListBoxItem()
            {
                Content = new Label()
                {
                    Content = "Archive"
                },
            };
            archiveItem.Selected += ArchiveOpenEvent;
            categoriesListBox.Items.Add(archiveItem);
        }

        private void ArchiveOpenEvent(object sender, RoutedEventArgs e)
        {
            selectedCategoryId = string.Empty;
            LoadArchive();
        }

        private void LoadArchive()
        {
            var archivedProjects = _services.Get<IProjectsService>()
                .GetProjectsList()
                .Where(p=>p.IsArchived)
                .ToList();
            LoadProjectsItemsToList(archivedProjects);
        }

        private void LoadProjects(string categoryId)
        {
            var projects = _services.Get<ICategoriesService>().GetCategoryWithProjects(categoryId)
                .Projects
                .Where(p=>p.IsArchived == false).ToList() ?? [];

            LoadProjectsItemsToList(projects);
        }

        private void LoadProjectsItemsToList(List<ProjectInfoModel> projects)
        {
            projectsListBox.Items.Clear();
            foreach (var project in projects)
            {
                var item = new ListBoxItem();
                item.Uid = project.Id;
                item.Selected += ProjectSelectedEvent;

                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var label = new Label() { Content = project.Name };

                var editBtn = new Button();
                editBtn.Uid = project.Id;
                if (!project.IsArchived)
                {
                    editBtn.Content = GetIcon("editIcon");
                    editBtn.Click += EditProjectEvent;
                }
                else
                {
                    editBtn.Content = GetIcon("unarchiveIcon");
                    editBtn.Click += UnarchiveProjectEvent;
                }

                var deleteBtn = new Button();
                deleteBtn.Uid = project.Id;
                if (!project.IsArchived)
                {
                    deleteBtn.Click += ArchiveProjectEvent;
                    deleteBtn.Content = GetIcon("archiveIcon");
                }
                else
                {
                    deleteBtn.Click += DeleteProjectEvent;
                    deleteBtn.Content = GetIcon("deleteIcon");
                }

                stack.Children.Add(label);
                stack.Children.Add(editBtn);
                stack.Children.Add(deleteBtn);

                item.Content = stack;
                projectsListBox.Items.Add(item);
            }
        }

        private void LoadTasks(string projectId)
        {
            doneTasksListBox.Items.Clear();
            undoneTasksListBox.Items.Clear();

            var tasks = _services.Get<ITasksPresenter>().GetProjectWithTasks(projectId).Tasks;
            tasks.Reverse();
            foreach (var task in tasks)
            {
                var item = new ListBoxItem();
                item.Uid = task.Id;
                item.MouseDoubleClick += TaskSelectedEvent;

                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var statusBtn = new Button();
                statusBtn.Uid = task.Id;
                var label = new Label() 
                { 
                    Content = task.Text 
                    + "- DueTo:" + task.DueTo.ToString() 
                    + " (" + task.StartTime.ToString() 
                    + "-" + task.EndTime.ToString() + ")"
                };

                var staticRes = new StaticResourceExtension();
                if (task.IsDone)
                {
                    statusBtn.Content = GetIcon("checkedIcon");
                    statusBtn.Click += MarkTaskUndoneEvent;
                }
                else
                {
                    statusBtn.Content = GetIcon("uncheckedIcon");
                    statusBtn.Click += MarkTaskDoneEvent;
                }

                stack.Children.Add(statusBtn);
                stack.Children.Add(label);
                item.Content = stack;

                if (task.IsDone)
                    doneTasksListBox.Items.Add(item);
                else 
                    undoneTasksListBox.Items.Add(item);

            }
        }

        private void TaskSelectedEvent(object sender, RoutedEventArgs e)
        {
            var id = ((ListBoxItem)sender).Uid;
            var window = new TaskDetailsWindow(id, _services);
            window.ShowDialog();
            LoadTasks(selectedProjectId);
        }

        private void MarkTaskUndoneEvent(object sender, RoutedEventArgs e)
        {
            var taskId = ((Button)sender).Uid;
            ChangeTaskStatus(taskId, false);
            LoadTasks(selectedProjectId);
        }

        private void MarkTaskDoneEvent(object sender, RoutedEventArgs e)
        {
            var taskId = ((Button)sender).Uid;
            ChangeTaskStatus(taskId, true);
            LoadTasks(selectedProjectId);
        }

        private void ChangeTaskStatus(string taskId, bool status)
        {
            var service = _services.Get<ITasksService>();
            var task = service.GetTaskById(taskId);
            task.IsDone = status;
            service.UpdateTask(task);
        }

        private void ProjectSelectedEvent(object sender, RoutedEventArgs e)
        {
            var id = ((ListBoxItem)sender).Uid;
            selectedProjectId = id;
            LoadTasks(id);
        }

        private void DeleteProjectEvent(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Uid;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this project?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _services.Get<IProjectsService>().DeleteProject(id);
                LoadArchive();
            }
        }

        private void ArchiveProjectEvent(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Uid;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to archive this project?", "Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var service = _services.Get<IProjectsService>();
                service.Archive(service.GetProjectById(id));
                LoadProjects(selectedCategoryId);
            }
        }

        private void UnarchiveProjectEvent(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Uid;
            var service = _services.Get<IProjectsService>();
            service.Unarchive(service.GetProjectById(id));
            LoadArchive();
        }

        private void EditProjectEvent(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Uid;
            MessageBox.Show("EditProjectWindow");
        }

        private void CategorySelectedEvent(object sender, RoutedEventArgs e)
        {
            var id = ((ListBoxItem)sender).Uid;
            selectedCategoryId = id;
            LoadProjects(id);
        }

        private void DeleteCategoryEvent(object sender, RoutedEventArgs e)
        {
            var categoryId = ((Button)sender).Uid;
            var service = _services.Get<ICategoriesService>();
            var category = service.GetCategoryWithProjects(categoryId);
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this category (" + category.Name + ")?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                service.DeleteCategory(category);
                LoadCategories();
            }
        }

        private void EditCategoryEvent(object sender, RoutedEventArgs e)
        {
            var categoryId = ((Button)sender).Uid;
            var window = new EditCategoryWindow(_services, categoryId);
            window.ShowDialog();
            LoadCategories();
        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewTaskWindow(_services, selectedProjectId);
            window.ShowDialog();
            LoadTasks(selectedProjectId);
        }

        private void addProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewProjectWindow(_services, selectedCategoryId);
            window.ShowDialog();
            LoadProjects(selectedCategoryId);
        }

        private void addCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewCategoryWindow(_services);
            window.ShowDialog();
            LoadCategories();
        }

        private Image GetIcon(string name)
        {
            return new Image()
            {
                Source = (BitmapImage)Application.Current.Resources[name],
                Height = 20,
                Width = 20
            };
        }
    }
}
