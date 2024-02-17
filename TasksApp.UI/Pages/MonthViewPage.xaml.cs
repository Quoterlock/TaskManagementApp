using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
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
using TasksApp.UI.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for MonthViewPage.xaml
    /// </summary>
    public partial class MonthViewPage : Page
    {
        ServicesContainer _services;
        private Dictionary<DateTime, List<TaskModel>> _tasks = [];
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
            DateTime date = new DateTime(_selectedYear, _selectedMonth, 1);
            string monthName = date.ToString("MMMM");
            selectedMonthLabel.Content = monthName + " (" + date.ToString("MM") + "), " + _selectedYear.ToString();
            LoadTasks();
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            calendarCanvas.Children.Clear();
            int height = (int)calendarCanvas.ActualHeight;
            int width = (int)calendarCanvas.ActualWidth;

            int rows = 6; int colns = 7;


            int blockHeight = height / rows;
            int blockWidth = width / colns;

            int baseTaskHeight = 26;

            int defaultTasksCount = blockHeight / baseTaskHeight;

            int leftCellMargin = 25;
            int taskWidth = blockWidth - leftCellMargin;
            if (taskWidth < 0) taskWidth = 0;

            calendarCanvas.Background = GetColor("calendarBackgroundColor");

            var daysList = new List<DayOfWeek> {
                DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday,
                DayOfWeek.Friday, DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            int firstDayIndex = daysList.FindIndex(d => d == _tasks.First().Key.DayOfWeek);

            // draw cells and numbers
            int dayCounter = 0;
            for (int i = 0; i < 6; i++)
            {
                int startPos = (i == 0) ? firstDayIndex : 0;
                for (int j = startPos; j < 7; j++)
                {
                    // draw cell background
                    if (dayCounter < _tasks.Count)
                    {
                        var cell = new Rectangle();
                        cell.Height = blockHeight;
                        cell.Width = blockWidth;

                        if(dayCounter == DateTime.Now.Day && _selectedYear == DateTime.Now.Year 
                            && _selectedMonth == DateTime.Now.Month)
                        {
                            cell.Fill = GetColor("todayHighlightColor");
                        } 
                        else
                        {
                            cell.Fill = Brushes.White;
                        }

                        Canvas.SetLeft(cell, j * blockWidth);
                        Canvas.SetTop(cell, i * blockHeight);
                        calendarCanvas.Children.Add(cell);
                       
                        // draw number
                        var label = new Label();
                        label.Content = (dayCounter + 1).ToString();
                        Canvas.SetLeft(label, j * blockWidth + 2);
                        Canvas.SetTop(label, i * blockHeight + 2);
                        calendarCanvas.Children.Add(label);

                        dayCounter++;
                    }
                }
            }

            // draw tasks

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < colns; j++)
                {

                }
            }


            foreach(var day in _tasks)
            {
                int stepBtwTasks = baseTaskHeight;
                if (day.Value.Count > 1)
                {
                    int step = (blockHeight - baseTaskHeight) / (day.Value.Count - 1);
                    if (step < stepBtwTasks)
                        stepBtwTasks = step;
                }

                for(int i = 0; i < day.Value.Count; i++)
                {
                    var dayOfTheMonth = day.Value[i].DueTo.Day;

                    var colnIndex = (dayOfTheMonth - 1 + firstDayIndex) % colns;
                    var rowIndex = ((dayOfTheMonth - 1 + firstDayIndex) - colnIndex) / colns;

                    int posX = colnIndex * blockWidth + leftCellMargin;
                    var posY = rowIndex * blockHeight + stepBtwTasks * i;

                    var item = GetTaskItem(day.Value[i]);
                    item.Width = taskWidth;
                    item.Height = baseTaskHeight;
                    Canvas.SetLeft(item, posX);
                    Canvas.SetTop(item, posY);
                    calendarCanvas.Children.Add(item);
                }
            }


            // draw grid
            // horizontal lines
            for (int i = 0; i < 6; i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = 0; line.X2 = width;
                line.Y1 = 0; line.Y2 = 0;
                line.Stroke = ResourceManager.GetColor("gridColor");
                Canvas.SetTop(line, i * blockHeight);
                calendarCanvas.Children.Add(line);
            }
            // vertical lines
            for (int i = 0; i < 7; i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = 0; line.X2 = 0;
                line.Y1 = 0; line.Y2 = height;
                line.Stroke = ResourceManager.GetColor("gridColor");
                Canvas.SetLeft(line, i * blockWidth);
                calendarCanvas.Children.Add(line);
            }
        }

        private void TaskClickedEvent(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Control)sender;
            var taskWindow = new TaskDetailsWindow(taskLabel.Uid, _services);
            taskWindow.ShowDialog();
            if (taskWindow.IsModified) UpdateState();
        }

        private Label GetTaskItem(TaskModel taskModel)
        {
            var label = new Label() 
            { 
                Content = (taskModel.IsDone ? "✓ " : "") + taskModel.Text 
            };
            var brush = new SolidColorBrush() {
                Color = (Color)ColorConverter.ConvertFromString(taskModel.Project.ColorHex),
            };
            label.Background = brush;
            label.BorderThickness = new Thickness(1);
            label.Uid = taskModel.Id;
            label.MouseDown += TaskClickedEvent;
            return label;
        }

        private void LoadTasks()
        {
            _tasks = _services.Get<ITasksPresenter>().GetByMonth(_selectedYear, _selectedMonth, true);
        }

        private void calendarCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCanvas();
        }

        private Brush GetRandomBlockColor()
        {
            Random rnd = new Random();
            int index = rnd.Next(6);     // creates a number between 0 and 51
            return GetColor("timeBlockColor" + index.ToString());
        }

        private Brush GetColor(string key)
        {
            return (SolidColorBrush)System.Windows.Application.Current.Resources[key];
        }

        private void nextMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedMonth++;
            if (_selectedMonth > 12)
            {
                _selectedMonth = 1;
                _selectedYear++;
            }
            UpdateState();
        }

        private void prevMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedMonth--;
            if(_selectedMonth < 1)
            {
                _selectedMonth = 12;
                _selectedYear--;
            }
            UpdateState();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }
    }
}
