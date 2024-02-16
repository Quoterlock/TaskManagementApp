using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for MonthViewPage.xaml
    /// </summary>
    public partial class MonthViewPage : Page
    {
        ServicesContainer _services;
        private Dictionary<DateTime, List<TaskModel>> _tasks;
        private int _selectedYear;
        private int _selectedMonth;
        public MonthViewPage(ServicesContainer services)
        {
            _services = services;
            _selectedYear = DateTime.Now.Year;
            _selectedMonth = DateTime.Now.Month;
            InitializeComponent();

            UpdateState();
        }

        private void UpdateState()
        {
            LoadTasks();
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            // draw grid
            // horizontal lines
            for(int i = 0; i < 6; i++)
            {

            }
            // vertical lines
            for (int i = 0; i < 6; i++)
            {

            }
        }

        private void LoadTasks()
        {
            _tasks = _services.Get<ITasksPresenter>().GetByMonth(_selectedYear, _selectedMonth, true);
        }
    }
}
