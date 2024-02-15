using System.Windows;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows
{
    public partial class NewProjectWindow : Window
    {
        private ServicesContainer _services;
        private string _categoryId;
        public NewProjectWindow(ServicesContainer services, string categoryId)
        {
            InitializeComponent();
            _services = services;
            _categoryId = categoryId;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _services.Get<IProjectsService>().AddProject(projectNameTextBox.Text, _categoryId);
                this.Close();
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
