using System;
using System.Collections.Generic;
using System.Globalization;
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
using TasksApp.UI.Windows;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for WeekViewPage.xaml
    /// </summary>
    public partial class WeekViewPage : Page
    {
        private ServicesContainer _services;
        private Dictionary<DateTime, List<TaskModel>> _tasks;
        public WeekViewPage(ServicesContainer services)
        {
            _services = services;
            InitializeComponent();
            LoadTasks();
            UpdateCanvas();
        }

        private void LoadTasks()
        {
            var presenter = _services.Get<ITasksPresenter>();

            DateTime currentDate = DateTime.Now;
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            System.Globalization.Calendar calendar = cultureInfo.Calendar;
            int weekNumber = calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            
            _tasks = presenter.GetByWeek(currentDate.Year, weekNumber, false);
        }

        private void UpdateCanvas()
        {
            mainCanvas.Children.Clear();
            int margin = 40;
            int canvasWidth = (int)mainCanvas.ActualWidth - margin;
            int minBlockSize = 28; // 15 min
            int canvasHeight = minBlockSize * 4 * 24;
            mainCanvas.Height = canvasHeight;
            int blockWidth = canvasWidth / 7;
            if (canvasWidth <= 0)
            {
                canvasWidth = 1;
                blockWidth = 1;
            }

            // display grid

            // horizontal lines
            var timeSteps = TimeOnly.Parse("00:00");
            for (int i = 0; i < canvasHeight/(minBlockSize*2); i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = margin; line.X2 = canvasWidth;
                line.Y1 = 0; line.Y2 = 0;
                Canvas.SetTop(line, i * minBlockSize * 2);
                //line.Y1 = i*minBlockSize*2; line.Y2 = line.Y1;
                
                line.Stroke = Brushes.Gray;
                mainCanvas.Children.Add(line);

                //add time to line
                var timeLabel = new Label();
                timeLabel.Content = timeSteps.ToString();
                int posY = i * minBlockSize * 2 - 14;
                Canvas.SetTop(timeLabel, posY);
                Canvas.SetLeft(timeLabel, 3);
                mainCanvas.Children.Add(timeLabel);

                timeSteps = timeSteps.AddMinutes(30);
            }

            // vertical lines
            for (int i = 1; i < canvasWidth/blockWidth; i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = i * blockWidth + margin; line.X2 = line.X1;
                line.Y1 = 0; line.Y2 = canvasHeight;
                line.Stroke = Brushes.Gray;
                mainCanvas.Children.Add(line);
            }

            // display tasks
            int dayCounter = 0;
            foreach (var pair in _tasks)
            {
                // add title 
                foreach(var task in pair.Value)
                {
                    // draw task on canvas
                    var label = new Label();
                    label.Content = task.Text + "(" + task.StartTime.ToString() + " - " + task.EndTime.ToString() + ")";
                    label.Width = blockWidth;
                    label.Uid = task.Id;
                    label.MouseDown += TaskClicked;
                    label.Background = Brushes.Red;
                    label.Height = CalculateHeightByDuration((int)(task.EndTime - task.StartTime).TotalMinutes, canvasHeight);
                    if (label.Height < 2) label.Height = minBlockSize;
                    Canvas.SetTop(label, PosByTime(task.StartTime.Hour, task.StartTime.Minute, canvasHeight));
                    Canvas.SetLeft(label, blockWidth * dayCounter + margin);
                    mainCanvas.Children.Add(label);
                }
                dayCounter++;
            }
        }

        private int PosByTime(int hour, int minutes, int height)
        {
            // було так => return height / (24 * 60) * (hour * 60 + minutes);
            return height * (hour * 60 + minutes) / (24 * 60);
        }

        private int CalculateHeightByDuration(int minutes, int totalHeight)
        {
            return totalHeight * minutes / (24 * 60);
        }

        private void mainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCanvas();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
            UpdateCanvas();
        }

        private void TaskClicked(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Label)sender;
            var taskWindow = new TaskDetailsWindow(taskLabel.Uid, _services);
            taskWindow.ShowDialog();
        }
    }
}
