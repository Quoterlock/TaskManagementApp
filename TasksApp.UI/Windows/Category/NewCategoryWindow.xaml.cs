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
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows
{
    public partial class NewCategoryWindow : Window
    {
        private ServicesContainer _services;
        public NewCategoryWindow(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    _services.Get<ICategoriesService>().AddCategory(categoryNameTextBox.Text);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Write correct name");
        }

        private bool CheckInput()
        {
            return true;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
