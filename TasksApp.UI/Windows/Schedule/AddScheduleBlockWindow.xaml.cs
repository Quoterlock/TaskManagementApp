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
using TasksApp.BusinessLogic.Services;
using TasksApp.DataAccess.Interfaces;
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows.Schedule
{
    /// <summary>
    /// Interaction logic for AddSheduleBlockWindow.xaml
    /// </summary>
    public partial class AddScheduleBlockWindow : Window
    {
        private readonly ServicesContainer _services;
        public bool IsModified { get; set; }
        public AddScheduleBlockWindow(ServicesContainer services)
        {
            InitializeComponent();
            IsModified = false;
            _services = services;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var block = new ScheduleBlockModel();
            if (CheckInput())
            {
                // get days
                if (moCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Monday);
                if (tuCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Tuesday);
                if (weCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Wednesday);
                if (thCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Thursday);
                if (frCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Friday);
                if (saCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Saturday);
                if (suCheckBox.IsChecked ?? false)
                    block.DaysOfWeek.Add(DayOfWeek.Sunday);

                block.Text = titleTextBox.Text;

                block.StartTime = TimeOnly.Parse(startTimeTextBox.Text);
                block.EndTime = TimeOnly.Parse(endTimeTextBox.Text);

                try
                {
                    _services.Get<IScheduleService>().AddBlock(block);
                    IsModified = true;
                    this.Close();
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Execution error");
                }
            }
        }

        private bool CheckInput()
        {
            try
            {
                var time = TimeOnly.Parse(startTimeTextBox.Text);
                var time2 = TimeOnly.Parse(startTimeTextBox.Text);
                if (time > time2)
                {
                    MessageBox.Show("End time can't be smaller that start time");
                    return false;
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Write time in a correct format (HH:MM)");
                return false;
            }
            return true;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
