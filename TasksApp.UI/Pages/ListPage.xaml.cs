using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private List<ProjectInfoModel> archivedProjects;
        private List<CategoryModel> activeCategories;
        private readonly ServicesContainer _services;
        public ListPage(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
            archivedProjects = [];
            LoadProjects();
        }

        private void LoadProjects()
        {
            var projectsService = _services.Get<IProjectsService>();
            var categoriesService = _services.Get<ICategoriesService>();

            archivedProjects = projectsService.GetProjectsList().Where(p=>p.IsArchived == true).ToList();
            
            var catList = categoriesService.GetList();
            foreach(var category in catList)
                activeCategories.Add(categoriesService.GetCategoryWithProjects(category.Id));

            var activeFolder = new TreeViewItem();
            activeFolder.Header = "Active";
            foreach (var category in activeCategories)
            {
                var categoryItem = new TreeViewItem();
                categoryItem.Header = category.Name;
                foreach (var project in category.Projects)
                {
                    var projectItem = new TreeViewItem();
                    projectItem.Header = project.Name;
                    projectItem.DataContext = project.Id;
                    categoryItem.Items.Add(projectItem);
                }
                activeFolder.Items.Add(categoryItem);
            }

            var archivedFolder = new TreeViewItem();
            archivedFolder.Header = "Archive";

            foreach (var projectInfo in archivedProjects)
            {
                var projectItem = new TreeViewItem();
                projectItem.Header = projectInfo.Name;
                projectItem.Uid = projectInfo.Id;
                archivedFolder.Items.Add(projectItem);
            }

            projectsTreeView.Items.Clear();
            projectsTreeView.Items.Add(activeFolder);
            projectsTreeView.Items.Add(archivedFolder);
        }

        private ProjectModel GetProjectWithTasks(string projectId)
        {
            var service = _services.Get<ITasksPresenter>();
            return service.GetProjectWithTasks(projectId);
        }

        private void projectsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            projectTasksListView.Items.Clear();
            var projectItem = (TreeViewItem)e.NewValue;
            var projectId = (string)projectItem.DataContext;
            if(projectId != null)
            {
                var project = GetProjectWithTasks(projectId);
                
                // TODO: Create two lists for done and undone
                // TODO: Create fields to display project info
                foreach (var task in project.Tasks)
                {
                    projectTasksListView.Items.Add(task.Text + " - DueTo: " + task.DueTo.ToString() + ", " + task.StartTime.ToString() + " - " + task.EndTime.ToString());
                }
            }
        }
    }
}
