using System.Windows;
using System.Windows.Controls;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private Dictionary<string, List<ProjectModel>> projects;
        private Dictionary<string, List<ProjectModel>> archivedProjects;
        private ServicesContainer _services;
        public ListPage(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
            LoadProjects();
        }

        private void LoadProjects()
        {
            var service = _services.Get<IProjectsService>();
            archivedProjects = service.GetAllGrouped(true);
            projects = service.GetAllGrouped(false);

            var activeFolder = new TreeViewItem();
            activeFolder.Header = "Active";
            foreach (var category in projects)
            {
                var categoryItem = new TreeViewItem();
                categoryItem.Header = category.Key;
                foreach (var project in category.Value)
                {
                    var projectItem = new TreeViewItem();
                    projectItem.Header = project.Name;
                    projectItem.DataContext = project;
                    projectItem.MouseLeftButtonDown += ProjectsThreeItemClick;
                    categoryItem.Items.Add(projectItem);
                }
                activeFolder.Items.Add(categoryItem);
            }

            var archivedFolder = new TreeViewItem();
            archivedFolder.Header = "Archive";
            foreach (var category in archivedProjects)
            {
                var categoryItem = new TreeViewItem();
                categoryItem.Header = category.Key;
                foreach(var project in category.Value)
                {
                    var projectItem = new TreeViewItem();
                    projectItem.Header = project.Name;
                    projectItem.DataContext = project;
                    projectItem.Selected += ProjectsThreeItemClick;
                    categoryItem.Items.Add(projectItem);
                }
                archivedFolder.Items.Add(categoryItem);
            }

            projectsTreeView.Items.Clear();
            projectsTreeView.Items.Add(activeFolder);
            projectsTreeView.Items.Add(archivedFolder);
        }

        private void ProjectsThreeItemClick(object sender, RoutedEventArgs e)
        {
            projectTasksListView.Items.Clear();
            var projectItem = (TreeViewItem)sender;
            var project = (ProjectModel)projectItem.DataContext;
            var tasks = LoadTasksForProject(project);
            foreach (var group in tasks) 
            {
                foreach (var task in group.Value) {
                    var item = new ListViewItem();
                    item.DataContext = task.Text;
                    projectTasksListView.Items.Add(item);
                }
            }
            
        }

        private Dictionary<TaskStatusEnum, List<TaskModel>> LoadTasksForProject(ProjectModel project)
        {
            var service = _services.Get<ITasksPresenter>();
            return service.GetByProject(project);
        }

        private void projectsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            projectTasksListView.Items.Clear();
            var projectItem = (TreeViewItem)e.NewValue;
            var project = (ProjectModel)projectItem.DataContext;
            if(project != null)
            {
                var tasks = LoadTasksForProject(project);
                foreach (var group in tasks)
                {
                    foreach (var task in group.Value)
                    {
                        projectTasksListView.Items.Add(task.Text + " - DueTo: " + task.DueTo.ToString() + ", " + task.StartTime.ToString() + " - " + task.EndTime.ToString());
                    }
                }
            }
        }
    }
}
