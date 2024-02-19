using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TasksApp.UI.Windows.Schedule
{
    /// <summary>
    /// Interaction logic for EditScheduleBlockWindow.xaml
    /// </summary>
    public partial class EditScheduleBlockWindow : Window
    {
        private readonly ServicesContainer _services;
        private ScheduleBlockModel _block = new();
        
        public bool IsModified { get; set; }    
        public EditScheduleBlockWindow(ServicesContainer services, string id)
        {
            InitializeComponent();
            _services = services;
            IsModified = false;
            TryGetBlock(id);
            if(!string.IsNullOrEmpty(_block.Id))
            {
                titleTextBox.Text = _block.Text;
                startTimeTextBox.Text = _block.StartTime.ToString();
                endTimeTextBox.Text = _block.EndTime.ToString();
                var days = _block.DaysOfWeek;
                if (days.Contains(DayOfWeek.Monday))
                    moCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Tuesday))
                    tuCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Wednesday))
                    weCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Thursday))
                    thCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Friday))
                    frCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Saturday))
                    saCheckBox.IsChecked = true;
                if (days.Contains(DayOfWeek.Sunday))
                    suCheckBox.IsChecked = true;
            }
        }

        private void TryGetBlock(string id)
        {
            try
            {
                if (id != null)
                {
                    _block = _services.Get<IScheduleService>()
                        .GetCurrentSchedule()
                        .FirstOrDefault(b => b.Id == id) ?? new ScheduleBlockModel();
                }
                else
                {
                    MessageBox.Show("Id is null");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                _block.DaysOfWeek.Clear();
                // get days
                if (moCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Monday);
                if (tuCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Tuesday);
                if (weCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Wednesday);
                if (thCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Thursday);
                if (frCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Friday);
                if (saCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Saturday);
                if (suCheckBox.IsChecked ?? false)
                    _block.DaysOfWeek.Add(DayOfWeek.Sunday);

                _block.Text = titleTextBox.Text;

                _block.StartTime = TimeOnly.Parse(startTimeTextBox.Text);
                _block.EndTime = TimeOnly.Parse(endTimeTextBox.Text);

                try
                {
                    _services.Get<IScheduleService>().UpdateBlock(_block);
                    IsModified = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Execution error");
                }
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
