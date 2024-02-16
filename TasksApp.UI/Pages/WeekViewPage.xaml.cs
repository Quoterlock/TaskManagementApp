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
using System.Xml.Linq;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;
using TasksApp.UI.Windows;
using TasksApp.UI.Windows.Schedule;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for WeekViewPage.xaml
    /// </summary>
    public partial class WeekViewPage : Page
    {
        private ServicesContainer _services;
        private Dictionary<DateTime, List<TaskModel>> _tasks = [];
        private Dictionary<DateTime, List<ScheduleTaskModel>> _scheduleTasks = [];
        private int _selectedWeek;
        private int _selectedYear;

        public WeekViewPage(ServicesContainer services)
        {
            _services = services;
            _selectedWeek = GetWeekNumber(DateTime.UtcNow);
            _selectedYear = DateTime.UtcNow.Year;

            InitializeComponent();
            UpdateState();
        }

        private void UpdateState()
        {
            LoadTasks();
            LoadSchedule();
            currentWeek.Content = "Week " + _selectedWeek.ToString() + ", " + _selectedYear.ToString();
            DisplayNotTimeBlockedTasks();
            DisplayTasksOnCanvas();

        }

        private void LoadTasks()
        {
            var presenter = _services.Get<ITasksPresenter>();     
            _tasks = presenter.GetByWeek(_selectedYear, _selectedWeek, false);
        }

        private void LoadSchedule()
        {
            _scheduleTasks = _services.Get<ITasksPresenter>().GetScheduleTasksByWeek(_selectedYear, _selectedWeek);
        }

        private void DisplayNotTimeBlockedTasks()
        {
            int itemHeight = 28;

            var listsArray = new ListBox[] { moListBox, tuListBox, weListBox, thListBox, frListBox, saListBox, suListBox };
            foreach (var list in listsArray)
                list.Items.Clear();

            var labelsArray = new Label[] { moLabel, tuLabel, weLabel, thLabel, frLabel, saLabel, suLabel };

            var dayCounter = 0;

            // display not time blocked tasks in list
            foreach (var day in _tasks)
            {
                labelsArray[dayCounter].Content = day.Key.ToString("dd.MM.yyyy") + " (" + day.Key.DayOfWeek.ToString() + ")";
                foreach (var task in day.Value)
                {
                    if (task.StartTime == task.EndTime && task.StartTime == TimeOnly.MinValue)
                    {
                        var item = new ListBoxItem();
                        var label = new Label();
                        label.Content = task.Text;
                        label.Height = itemHeight;
                        label.Background = GetRandomBlockColor();
                        item.Uid = task.Id;
                        item.Content = label;
                        item.Height = itemHeight;
                        item.MouseDoubleClick += TaskClicked;

                        listsArray[dayCounter].Items.Add(item);
                    }
                }
                dayCounter++;
            }

            // find max tasks count
            int maxCount = 0;
            foreach (var list in listsArray)
                if (maxCount < list.Items.Count)
                    maxCount = list.Items.Count;
            notTimeBlockedTasksRow.Height = new GridLength((maxCount + 1) * itemHeight + itemHeight);
        }

        private void DisplayTasksOnCanvas()
        {
            // draw tasks on canvas
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

            // display schedule
            int dayCounter = 0;
            foreach (var pair in _scheduleTasks)
            {
                foreach(var task in pair.Value)
                {
                    // draw task on canvas
                    var label = new Label();
                    label.Content = task.Text + "(" + task.StartTime.ToString() + " - " + task.EndTime.ToString() + ")";
                    label.Width = blockWidth;
                    label.Uid = task.Id;
                    label.MouseDown += ScheduleTaskClicked;
                    label.Background = GetColor("scheduleBlockColor");
                    label.Height = CalculateHeightByDuration((int)(task.EndTime - task.StartTime).TotalMinutes, canvasHeight);
                    if (label.Height < 2) label.Height = minBlockSize;
                    Canvas.SetTop(label, PosByTime(task.StartTime.Hour, task.StartTime.Minute, canvasHeight));
                    Canvas.SetLeft(label, blockWidth * dayCounter + margin);
                    mainCanvas.Children.Add(label);
                }
                dayCounter++;
            }
            
            // display tasks
            dayCounter = 0;
            foreach (var pair in _tasks)
            {
                // add title 
                foreach (var task in pair.Value)
                {
                    // if it is blocked
                    if (task.StartTime != task.EndTime)
                    {
                        var label = new Label();
                        label.Content = task.Text + "(" + task.StartTime.ToString() + " - " + task.EndTime.ToString() + ")";
                        label.Width = blockWidth;
                        label.Uid = task.Id;
                        label.MouseDown += TaskClicked;
                        label.Background = GetRandomBlockColor();
                        if(task.EndTime == TimeOnly.MinValue && task.StartTime != task.EndTime)
                        {
                            label.Height = minBlockSize;
                        }
                        else
                        {
                            label.Height = CalculateHeightByDuration((int)(task.EndTime - task.StartTime).TotalMinutes, canvasHeight);
                            if (label.Height < 2) label.Height = minBlockSize;
                        }
                        Canvas.SetTop(label, PosByTime(task.StartTime.Hour, task.StartTime.Minute, canvasHeight));
                        Canvas.SetLeft(label, blockWidth * dayCounter + margin);
                        mainCanvas.Children.Add(label);
                    }
                }
                dayCounter++;
            }


            // draw current time line
            var dateTimeNow = DateTime.Now;
            if(_selectedWeek == GetWeekNumber(dateTimeNow) && _selectedYear == dateTimeNow.Year)
            {
                var daysList = new List<DayOfWeek> {
                DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday,
                DayOfWeek.Friday, DayOfWeek.Saturday,
                DayOfWeek.Sunday};

                int timeLinePosX1 = daysList.FindIndex(d=>d == dateTimeNow.DayOfWeek) * blockWidth + margin; int timeLinePosX2 = timeLinePosX1 + blockWidth;
                int timeLinePosY = PosByTime(dateTimeNow.Hour, dateTimeNow.Minute, canvasHeight);
                var line = new Line() { X1 = timeLinePosX1, X2 =  timeLinePosX2 };
                line.Stroke = Brushes.Red;
                line.StrokeThickness = 2;
                Canvas.SetTop(line, timeLinePosY);
                mainCanvas.Children.Add(line);
            }
        }

        private void mainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayTasksOnCanvas();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        private void TaskClicked(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Control)sender;
            var taskWindow = new TaskDetailsWindow(taskLabel.Uid, _services);
            taskWindow.ShowDialog();

            if (taskWindow.IsModified) UpdateState();
        }

        private void ScheduleTaskClicked(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Control)sender;
            var dialog = new ScheduleTaskDetailsWindow(_services, taskLabel.Uid);
            dialog.ShowDialog();

            if (dialog.IsModified) UpdateState();
        }

        private void applyScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EnterDateDialog("Enter start date to apply current schedule");
            dialog.ShowDialog();
            if (dialog.isConfirmed)
            {
                _services.Get<IScheduleService>().UpdateFutureTasksToScheme(dialog.date.ToDateTime(TimeOnly.MinValue));
                UpdateState();
            }
        }

        private void prevWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedWeek--;
            if (_selectedWeek < 0)
            {
                _selectedYear--;
                _selectedWeek = GetWeeksInYear(_selectedYear);
            }
            UpdateState();
        }

        private void nextWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedWeek++;
            if (_selectedWeek > GetWeeksInYear(_selectedYear))
            {
                _selectedYear++;
                _selectedWeek = 0;
            }
            UpdateState();
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

        public int GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }

        private int GetWeekNumber(DateTime date)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            System.Globalization.Calendar calendar = cultureInfo.Calendar;
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }

        private Brush GetColor(string key)
        {
            return (SolidColorBrush)Application.Current.Resources[key];
        }

        private Brush GetRandomBlockColor()
        {
            Random rnd = new Random();
            int index = rnd.Next(6);     // creates a number between 0 and 51
            return GetColor("timeBlockColor" + index.ToString());
        }
    }
}
