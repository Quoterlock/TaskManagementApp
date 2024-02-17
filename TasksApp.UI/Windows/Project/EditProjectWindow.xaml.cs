using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TasksApp.UI.Windows.Project
{
    /// <summary>
    /// Interaction logic for EditProjectWindow.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        ServicesContainer _services;
        ProjectModel _project;
        public EditProjectWindow(ServicesContainer services, string id)
        {
            _services = services;
            InitializeComponent();
            LoadProject(id);
        }

        private void LoadProject(string id)
        {
            try
            {
                _project = _services.Get<IProjectsService>().GetProjectById(id);
                projectNameTextBox.Text = _project.Name;
                colorTextBox.Text = _project.ColorHex;
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Define the regular expression pattern
            string pattern = @"#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})";
            bool isMatch = Regex.IsMatch(colorTextBox.Text, pattern);
            if (isMatch)
            {
                _project.ColorHex = colorTextBox.Text;
                _project.Name = projectNameTextBox.Text;
                try
                {
                    _services.Get<IProjectsService>().UpdateProject(_project);
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
