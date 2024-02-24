using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private readonly ServicesContainer _services;
        private Dictionary<DateTime, List<TaskModel>> _tasks = [];
        private Dictionary<DateTime, List<ScheduleTaskModel>> _scheduleTasks = [];
        
        private int _selectedWeek;
        private int _selectedYear;
        private int _scale = 100;

        private const int DEFAULT_BLOCK_SIZE = 24;
        private int _minBlockSize; // 15 min block size
        private int _canvasHeight;
        private int _canvasWidth;
        private const int _canvasLeftMargin = 40;
        private const int _blockMargin = 2;

        public WeekViewPage(ServicesContainer services)
        {
            _services = services;
            _selectedWeek = GetWeekNumber(DateTime.Now);
            _selectedYear = DateTime.Now.Year;
            _scale = 100;
            _canvasHeight = 0;
            _canvasWidth = 0;
            _minBlockSize = 0;

            InitializeComponent();

            // UpdateState() is in the event in this check-boxes
            showScheduleCheckBox.IsChecked = true;
            showTasksCheckBox.IsChecked = true;
        }

        private void UpdateState()
        {
            LoadTasks();
            LoadSchedule();
            currentWeek.Content = "Week " + _selectedWeek.ToString() + ", " + _selectedYear.ToString();
            scaleLabel.Content = _scale + "%";
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

        private void SetScrollViewOffset()
        {
            var dateTimeNow = DateTime.Now;
            int CurrentPosY = GetPositionByTime(dateTimeNow.Hour, dateTimeNow.Minute, (int)mainCanvas.ActualHeight);

            // set current line in screen center
            var scrollOffset = CurrentPosY - (int)scrollViewer.ActualHeight / 2;
            if (scrollOffset < 0)
                scrollOffset = 0;
            if (scrollOffset > (int)mainCanvas.ActualHeight)
                scrollOffset = (int)mainCanvas.ActualHeight;
            scrollViewer.ScrollToVerticalOffset(scrollOffset);
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
                // add label
                labelsArray[dayCounter].Content = day.Key.DayOfWeek.ToString() + " (" + day.Key.ToString("dd.MM.yyyy") + ")";
                foreach (var task in day.Value)
                {
                    if(task.IsScheduled && !task.IsTimeBlocked)
                    {
                        var item = GetTaskListItem(task, itemHeight);
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
            // set section height
            notTimeBlockedTasksRow.Height = new GridLength((maxCount + 1) * itemHeight + itemHeight);
        }

        private ListBoxItem GetTaskListItem(TaskModel task, int height)
        {
            var item = new ListBoxItem();
            var grid = new Grid();
            grid.Children.Add(new Rectangle
            {
                Fill = new SolidColorBrush()
                {
                    Color = (Color)ColorConverter.ConvertFromString(task.Project.ColorHex)
                },
                RadiusX = 3,
                RadiusY = 3
            });

            var label = new Label();
            label.Content = task.Text;
            var icon = ResourceManager.GetIcon(task.IsDone ? "checkedIcon" : "uncheckedIcon");
            icon.Width = 16; icon.Height = 16;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Children.Add(icon);
            stack.Children.Add(label);
            stack.Height = height;
            stack.Margin = new Thickness(3, 0, 3, 0);

            grid.Children.Add(stack);
            item.Content = grid;
            item.Uid = task.Id;
            item.Height = height;
            item.MouseDoubleClick += TaskClickedEvent;
            return item;
        }

        private void DisplayTasksOnCanvas()
        {
            mainCanvas.Children.Clear();
            
            _canvasWidth = (int)mainCanvas.ActualWidth - _canvasLeftMargin;
            _minBlockSize = (int)(DEFAULT_BLOCK_SIZE * ((float)_scale) / 100);
            _canvasHeight = _minBlockSize * 4 * 24;
            
            mainCanvas.Height = _canvasHeight;
            
            int blockWidth = _canvasWidth / 7;
            if (_canvasWidth <= 0)
            {
                _canvasWidth = 1;
                blockWidth = 1;
            }


            DrawBackground();

            DrawGrid(blockWidth);

            if (showScheduleCheckBox.IsChecked ?? false)
                DisplayScheduleTasks(blockWidth);    

            if(showTasksCheckBox.IsChecked ?? false)
                DisplayTasks(blockWidth);

            DrawCurrentTimeLine(blockWidth);
        }

        private void DrawBackground()
        {
            var backgroundRectangle = new Rectangle();
            backgroundRectangle.Width = _canvasWidth;
            backgroundRectangle.Height = _canvasHeight;
            backgroundRectangle.Fill = Brushes.White;
            backgroundRectangle.Uid = "background";
            mainCanvas.Children.Add(backgroundRectangle);
        }

        private void DrawGrid(int blockWidth)
        {
            // horizontal lines
            var timeSteps = TimeOnly.Parse("00:00");
            for (int i = 0; i < _canvasHeight / (_minBlockSize * 2); i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = _canvasLeftMargin; line.X2 = _canvasWidth;
                line.Y1 = 0; line.Y2 = 0;
                Canvas.SetTop(line, i * _minBlockSize * 2);
                //line.Y1 = i*minBlockSize*2; line.Y2 = line.Y1;

                line.Stroke = ResourceManager.GetColor("gridColor");
                mainCanvas.Children.Add(line);

                //add time to line
                var timeLabel = new Label();
                timeLabel.Content = timeSteps.ToString();
                int posY = i * _minBlockSize * 2 - 14;
                Canvas.SetTop(timeLabel, posY);
                Canvas.SetLeft(timeLabel, 3);
                mainCanvas.Children.Add(timeLabel);

                timeSteps = timeSteps.AddMinutes(30);
            }

            // vertical lines
            for (int i = 1; i < _canvasWidth / blockWidth; i++)
            {
                var line = new Line();
                line.StrokeThickness = 1;
                line.X1 = i * blockWidth + _canvasLeftMargin; line.X2 = line.X1;
                line.Y1 = 0; line.Y2 = _canvasHeight;
                line.Stroke = ResourceManager.GetColor("gridColor");
                mainCanvas.Children.Add(line);
            }
        }

        private void DisplayScheduleTasks(int blockWidth)
        {
            int dayCounter = 0;
            foreach (var pair in _scheduleTasks)
            {
                foreach (var task in pair.Value)
                {
                    // draw task on canvas
                    var label = new Label();
                    label.Content = task.Text + "(" + task.StartTime.ToString() + " - " + task.EndTime.ToString() + ")";
                    label.Width = blockWidth - _blockMargin * 2 < 0 ? 0 : blockWidth - _blockMargin * 2;
                    label.Uid = task.Id;
                    label.MouseDown += ScheduleTaskClickedEvent;
                    label.Background = ResourceManager.GetColor("scheduleBlockColor");
                    label.Height = CalculateHeightByDuration((int)(task.EndTime - task.StartTime).TotalMinutes, _canvasHeight);
                    if (label.Height < 2) label.Height = _minBlockSize;
                    label.Height -= _blockMargin * 2;
                    Canvas.SetTop(label, GetPositionByTime(task.StartTime.Hour, task.StartTime.Minute, _canvasHeight) + _blockMargin);
                    Canvas.SetLeft(label, blockWidth * dayCounter + _canvasLeftMargin + _blockMargin);
                    mainCanvas.Children.Add(label);
                }
                dayCounter++;
            }
        }

        private void DisplayTasks(int blockWidth)
        {
            int dayCounter = 0;
            foreach (var pair in _tasks)
            {
                foreach (var task in pair.Value)
                {
                    if (task.IsTimeBlocked)
                    {
                        var label = new Label();
                        label.Width = blockWidth - _blockMargin * 2 < 0 ? 0 : blockWidth - _blockMargin * 2;
                        label.Uid = task.Id;
                        label.MouseDown += TaskClickedEvent;
                        label.Background = new SolidColorBrush()
                        {
                            Color = (Color)ColorConverter.ConvertFromString(task.Project.ColorHex),
                            Opacity = 0.5
                        };

                        var text = (task.IsDone ? "✓ " : "") + task.Text;
                        if (task.EndTime == TimeOnly.MinValue)
                        {
                            label.Height = _minBlockSize;
                            text += " (" + task.StartTime.ToString() + ")";
                        }
                        else
                        {
                            text += " (" + task.StartTime.ToString()
                                + " - " + task.EndTime.ToString() + ")";
                            label.Height = CalculateHeightByDuration((int)(task.EndTime - task.StartTime).TotalMinutes, _canvasHeight);
                            if (label.Height < 2) label.Height = _minBlockSize;
                            label.Height -= _blockMargin * 2;
                        }
                        label.Content = text;

                        Canvas.SetTop(label, GetPositionByTime(task.StartTime.Hour, task.StartTime.Minute, _canvasHeight) + _blockMargin);
                        Canvas.SetLeft(label, blockWidth * dayCounter + _canvasLeftMargin + _blockMargin);
                        mainCanvas.Children.Add(label);
                    }
                }
                dayCounter++;
            }
        }

        private void DrawCurrentTimeLine(int blockWidth)
        {
            var dateTimeNow = DateTime.Now;

            if (_selectedWeek == GetWeekNumber(dateTimeNow) && _selectedYear == dateTimeNow.Year)
            {
                var daysList = new List<DayOfWeek> 
                { 
                    DayOfWeek.Monday, DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday, DayOfWeek.Thursday,
                    DayOfWeek.Friday, DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                };

                int timeLinePosX1 = daysList.FindIndex(d => d == dateTimeNow.DayOfWeek) * blockWidth + _canvasLeftMargin; 
                int timeLinePosX2 = timeLinePosX1 + blockWidth;
                int timeLinePosY = GetPositionByTime(dateTimeNow.Hour, dateTimeNow.Minute, _canvasHeight);
                
                var line = new Line() 
                { 
                    X1 = timeLinePosX1,
                    X2 = timeLinePosX2 
                };

                line.Stroke = Brushes.Red;
                line.StrokeThickness = 2;

                Canvas.SetTop(line, timeLinePosY);
                mainCanvas.Children.Add(line);
            }
        }

        private void CanvasSizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            DisplayTasksOnCanvas();
        }

        private void RefreshBtnClickEvent(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        private void TaskClickedEvent(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Control)sender;
            var taskWindow = new TaskDetailsWindow(taskLabel.Uid, _services);
            taskWindow.ShowDialog();

            if (taskWindow.IsModified) UpdateState();
        }

        private void ScheduleTaskClickedEvent(object sender, MouseButtonEventArgs e)
        {
            var taskLabel = (Control)sender;
            var dialog = new ScheduleTaskDetailsWindow(_services, taskLabel.Uid);
            dialog.ShowDialog();

            if (dialog.IsModified) UpdateState();
        }

        private void ApplyScheduleBtnClickEvent(object sender, RoutedEventArgs e)
        {
            var dialog = new EnterDateDialog("Enter start date to apply current schedule");
            dialog.ShowDialog();
            if (dialog.isConfirmed)
            {
                _services.Get<IScheduleService>().UpdateFutureTasksToScheme(dialog.date.ToDateTime(TimeOnly.MinValue));
                UpdateState();
            }
        }

        private void PreviousWeekBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _selectedWeek--;
            if (_selectedWeek < 0)
            {
                _selectedYear--;
                _selectedWeek = GetWeeksInYear(_selectedYear);
            }
            UpdateState();
        }

        private void NextWeekBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _selectedWeek++;
            if (_selectedWeek > GetWeeksInYear(_selectedYear))
            {
                _selectedYear++;
                _selectedWeek = 0;
            }
            UpdateState();
        }

        private void ShowCheckBoxChangedEvent(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        private void PageLoadedEvent(object sender, RoutedEventArgs e)
        {
            SetScrollViewOffset();
        }

        private void ScaleUpBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _scale += 10;
            if (_scale > 150)
                _scale = 150;
            scaleLabel.Content = _scale + "%";
            DisplayTasksOnCanvas();
        }

        private void ScaleDownBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _scale -= 10;
            if (_scale < 30)
                _scale = 30;
            scaleLabel.Content = _scale + "%";
            DisplayTasksOnCanvas();
        }

        private void CanvasMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(mainCanvas);
            if (e.Source.GetType() == typeof(Rectangle))
            {
                // if user clicked on an empty space
                var rect = (Rectangle)e.Source;
                if(rect.Uid == "background")
                {
                    // if user clicked not on margin
                    if(p.X > _canvasLeftMargin)
                    {
                        int blockWith = _canvasWidth / 7;
                        
                        // calculate position on timeline
                        int dayNum = ((int)p.X - _canvasLeftMargin) / (blockWith);
                        int timeMinutes = (int)p.Y * (24 * 60) / _canvasHeight;

                        // get date
                        DateTime date = GetMondayDate(_selectedWeek).AddDays(dayNum).AddHours(timeMinutes / 60).AddMinutes(timeMinutes % 60);
                        
                        // create task window
                        var dialog = new NewTaskWindow(_services, string.Empty, date);
                        dialog.ShowDialog();
                    }
                }
            }
        }

        private int GetPositionByTime(int hour, int minutes, int height)
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

        private DateTime GetMondayDate(int weekNum)
        {
            DateTime jan1 = new DateTime(_selectedYear, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            if (firstWeek <= 1)
                weekNum -= 1;
            return firstMonday.AddDays(weekNum * 7);
        }
    }
}
