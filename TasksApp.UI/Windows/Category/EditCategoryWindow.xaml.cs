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
using System.Windows.Shapes;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {
        private ServicesContainer _services;
        private CategoryModel _categoryModel;
        public EditCategoryWindow(ServicesContainer services, string categoryId)
        {
            InitializeComponent();
            _services = services;
            _categoryModel = _services.Get<ICategoriesService>().GetCategoryWithProjects(categoryId);
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _services.Get<ICategoriesService>().RenameCategory(_categoryModel, categoryNameTextBox.Text);
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
