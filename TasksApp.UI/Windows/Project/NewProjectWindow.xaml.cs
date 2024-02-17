using System.Text.RegularExpressions;
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
            // Define the regular expression pattern
            string pattern = @"#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})";
            bool isMatch = Regex.IsMatch(colorTextBox.Text, pattern);
            if (isMatch)
            {
                var project = new BusinessLogic.Models.ProjectInfoModel()
                {
                    ColorHex = colorTextBox.Text,
                    Name = projectNameTextBox.Text,
                    CategoryId = _categoryId
                };

                try
                {
                    _services.Get<IProjectsService>().AddProject(project);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Write color in a correct form (#RRGGBB)!");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
