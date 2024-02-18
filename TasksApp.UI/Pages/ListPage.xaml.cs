using System.Windows;
using System.Windows.Controls;
using TasksApp.UI.Services;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.UI.Windows;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Windows.Project;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private readonly ServicesContainer _services;
        private string _selectedCategoryId;
        private string selectedProjectId;

        public ListPage(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
            selectedProjectId = string.Empty;
            _selectedCategoryId = string.Empty;
            LoadCategories();
        }

        private void LoadCategories()
        {
            categoriesListBox.Items.Clear();
            var service = _services.Get<ICategoriesService>();
            var categories = service.GetList();

            // add uncategorised folder
            var noCategoryItem = new ListBoxItem()
            {
                Uid = string.Empty,
                Content = new Label()
                {
                    Content = "No-Category"
                },
            };
            noCategoryItem.Selected += CategorySelectedEvent;
            categoriesListBox.Items.Add(noCategoryItem);

            
            // add categories
            foreach (var category in categories)
            {
                ListBoxItem item = GetCategoryListItem(category);
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

        private ListBoxItem GetCategoryListItem(CategoryModel category)
        {
            var item = new ListBoxItem();
            item.Uid = category.Id;
            item.Selected += CategorySelectedEvent;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            var label = new Label() { Content = category.Name };

            var editBtn = new Button() { Uid = category.Id };
            var editIcon = ResourceManager.GetIcon("editIcon");
            editIcon.Height = 20; editIcon.Width = 20;
            editBtn.Content = editIcon;
            editBtn.Click += EditCategoryEvent;

            var deleteBtn = new Button() { Uid = category.Id };
            var deleteIcon = ResourceManager.GetIcon("deleteIcon");
            deleteIcon.Height = 20; deleteIcon.Width = 20;
            deleteBtn.Content = deleteIcon;
            deleteBtn.Click += DeleteCategoryEvent;

            stack.Children.Add(label);
            stack.Children.Add(editBtn);
            stack.Children.Add(deleteBtn);

            item.Content = stack;
            return item;
        }

        private void ArchiveOpenEvent(object sender, RoutedEventArgs e)
        {
            _selectedCategoryId = string.Empty;
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
            var projects = new List<ProjectInfoModel>();
            if (categoryId != string.Empty)
            {
                projects = _services.Get<ICategoriesService>().GetCategoryWithProjects(categoryId)
                    .Projects.Where(p => p.IsArchived == false).ToList() ?? [];
            }
            else
            {
                projects = _services.Get<IProjectsService>()
                    .GetProjectsList().Where(p => p.IsArchived == false && p.CategoryId == string.Empty)
                    .ToList() ?? [];
            }

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
                    var editIcon = ResourceManager.GetIcon("editIcon");
                    editIcon.Height = 20; editIcon.Width = 20;
                    editBtn.Content = editIcon;
                    editBtn.Click += EditProjectEvent;
                }
                else
                {
                    var icon = ResourceManager.GetIcon("unarchiveIcon");
                    icon.Height = 20; icon.Width = 20;
                    editBtn.Content = icon;
                    editBtn.Click += UnarchiveProjectEvent;
                }

                var deleteBtn = new Button();
                deleteBtn.Uid = project.Id;
                if (!project.IsArchived)
                {
                    deleteBtn.Click += ArchiveProjectEvent;

                    var icon = ResourceManager.GetIcon("archiveIcon");
                    icon.Height = 20; icon.Width = 20;
                    deleteBtn.Content = icon;
                }
                else
                {
                    deleteBtn.Click += DeleteProjectEvent;

                    var icon = ResourceManager.GetIcon("deleteIcon");
                    icon.Height = 20; icon.Width = 20;
                    deleteBtn.Content = icon;
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

                var label = new Label();

                var text = task.Text;
                if (task.IsScheduled)
                {
                    text += "- DueTo:" + task.DueTo.ToString();
                    if (task.IsTimeBlocked)
                        text += " (" + task.StartTime.ToString()
                            + "-" + task.EndTime.ToString() + ")";
                }
                label.Content = text;

                var staticRes = new StaticResourceExtension();
                if (task.IsDone)
                {
                    var icon = ResourceManager.GetIcon("checkedIcon");
                    icon.Height = 20; icon.Width = 20;
                    statusBtn.Content = icon;
                    statusBtn.Click += MarkTaskUndoneEvent;
                }
                else
                {
                    var icon = ResourceManager.GetIcon("uncheckedIcon");
                    icon.Height = 20; icon.Width = 20;
                    statusBtn.Content = icon;
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
                LoadProjects(_selectedCategoryId);
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
            var dialog = new EditProjectWindow(_services, id);
            dialog.ShowDialog();
            LoadProjects(_selectedCategoryId);
        }

        private void CategorySelectedEvent(object sender, RoutedEventArgs e)
        {
            var id = ((ListBoxItem)sender).Uid;
            _selectedCategoryId = id;
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
            var window = new NewProjectWindow(_services, _selectedCategoryId);
            window.ShowDialog();
            LoadProjects(_selectedCategoryId);
        }

        private void addCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewCategoryWindow(_services);
            window.ShowDialog();
            LoadCategories();
        }
    }
}
