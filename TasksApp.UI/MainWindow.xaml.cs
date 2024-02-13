using System.Threading.Tasks;
using System.Windows;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Services;
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
            services.Bind<ITasksRepository, MockTasksRepository>();
            services.Bind<IProjectsRepository, MockProjectsRepository>();
            services.Bind<ITasksService, TasksService>(services.Get<ITasksRepository>());
            services.Bind<IProjectsService, ProjectsService>(services.Get<IProjectsRepository>());
            services.Bind<ITasksPresenter, TasksPresenter>(services.Get<ITasksService>(), services.Get<IProjectsService>());
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
