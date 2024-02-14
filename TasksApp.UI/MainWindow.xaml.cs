using Microsoft.EntityFrameworkCore;
using System.Windows;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.BusinessLogic.Services;
using TasksApp.DataAccess;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;
using TasksApp.DataAccess.Repositories;
using TasksApp.UI.Pages;
using TasksApp.UI.Services;
using TasksApp.UI.Windows;

namespace TasksApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServicesContainer services;
        public MainWindow()
        {
            InitializeComponent();
            services = new ServicesContainer();
            SetupBuilder.CheckFiles();
            var dbContext = new AppDbContext();

            services.Bind<ITasksRepository, TasksRepository>(dbContext);
            services.Bind<IProjectsRepository, ProjectsRepository>(dbContext);
            services.Bind<ICategoriesRepository, CategoriesRepository>(dbContext);

            services.Bind<IAdapterME<TaskModel, TaskEntity>, TaskAdapter>();
            services.Bind<IAdapterME<ProjectModel, ProjectEntity>, ProjectAdapter>();
            services.Bind<IAdapterME<CategoryModel, CategoryEntity>, CategoryAdapter>();

            
            services.Bind<IProjectsService, ProjectsService>(
                services.Get<IProjectsRepository>(),
                services.Get<IAdapterME<ProjectModel, ProjectEntity>>());

            services.Bind<ITasksService, TasksService>(
                services.Get<ITasksRepository>(),
                services.Get<IProjectsService>(),
                services.Get<IAdapterME<TaskModel, TaskEntity>>());

            services.Bind<ICategoriesService, CategoriesService>(
                services.Get<ICategoriesRepository>(), 
                services.Get<IProjectsService>(),
                services.Get<IAdapterME<CategoryModel, CategoryEntity>>());

            services.Bind<ITasksPresenter, TasksPresenter>(
                services.Get<ITasksService>(),
                services.Get<IProjectsService>());
        }

        private void calendarBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new PresentationPage(services);
            mainFrame.NavigationService.RemoveBackEntry(); // clear history
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var createTaskDialog = new NewTaskWindow(services);
            createTaskDialog.ShowDialog();        
        }

        private void listBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ListPage(services);
            mainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
