using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasksApp.UI.Services;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for PresentationPage.xaml
    /// </summary>
    public partial class PresentationPage : Page
    {
        ServicesContainer _services;
        public PresentationPage(ServicesContainer services)
        {
            _services = services;
            InitializeComponent();
            // startPage
            viewFrame.Content = new MonthViewPage(_services);
            viewFrame.NavigationService.RemoveBackEntry();
        }

        private void monthViewBtn_Click(object sender, RoutedEventArgs e)
        {
            viewFrame.Content = new MonthViewPage(_services);
            viewFrame.NavigationService.RemoveBackEntry();
        }

        private void weekViewBtn_Click(object sender, RoutedEventArgs e)
        {
            viewFrame.Content = new WeekViewPage(_services);
            viewFrame.NavigationService.RemoveBackEntry();
        }
    }
}
