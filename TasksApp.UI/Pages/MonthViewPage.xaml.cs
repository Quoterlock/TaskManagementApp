using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;
using TasksApp.UI.Windows;

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

        private const int baseTaskHeight = 26;
        private const int leftCellMargin = 25;
        private const int rows = 6; 
        private const int columns = 7;

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
            selectedMonthLabel.Content = date.ToString("MMMM") + " (" + date.ToString("MM") + "), " + _selectedYear.ToString();
            LoadTasks();
            UpdateCanvas();
        }

        private void LoadTasks()
        {
            _tasks = _services.Get<ITasksPresenter>().GetByMonth(_selectedYear, _selectedMonth, true);
        }

        private void UpdateCanvas()
        {
            calendarCanvas.Children.Clear();

            int height = (int)calendarCanvas.ActualHeight;
            int width = (int)calendarCanvas.ActualWidth;

            int blockHeight = height / rows;
            int blockWidth = width / columns;

            int defaultTasksCount = blockHeight / baseTaskHeight;

            calendarCanvas.Background = ResourceManager.GetColor("calendarBackgroundColor");

            var daysList = new List<DayOfWeek>
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday,
                DayOfWeek.Friday, DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            int firstDayIndex = daysList.FindIndex(d => d == _tasks.First().Key.DayOfWeek);

            DrawCells(blockHeight, blockWidth, firstDayIndex);

            DisplayTasks(blockHeight, blockWidth, firstDayIndex);

            DrawGrid(height, width);
        }

        private void DrawCells(int blockHeight, int blockWidth, int firstDayIndex)
        {
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

                        // save the block date
                        DateOnly blockDate = new DateOnly(_selectedYear, _selectedMonth, dayCounter + 1);
                        cell.Uid = blockDate.ToString();

                        // color block
                        if (blockDate == DateOnly.FromDateTime(DateTime.Now))
                            cell.Fill = ResourceManager.GetColor("todayHighlightColor");
                        else
                            cell.Fill = Brushes.White;

                        // set position on canvas
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
        }

        private void DisplayTasks(int blockHeight, int blockWidth, int firstDayIndex)
        {
            int taskWidth = blockWidth - leftCellMargin;
            if (taskWidth < 0) taskWidth = 0;

            foreach (var day in _tasks)
            {
                int stepBtwTasks = baseTaskHeight;
                if (day.Value.Count > 1)
                {
                    int step = (blockHeight - baseTaskHeight) / (day.Value.Count - 1);
                    if (step < stepBtwTasks)
                        stepBtwTasks = step;
                }

                for (int i = 0; i < day.Value.Count; i++)
                {
                    var dayOfTheMonth = day.Value[i].DueTo.Day;

                    var colnIndex = (dayOfTheMonth - 1 + firstDayIndex) % columns;
                    var rowIndex = ((dayOfTheMonth - 1 + firstDayIndex) - colnIndex) / columns;

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
        }

        private void DrawGrid(int height, int width)
        {
            int blockHeight = height / rows;
            int blockWidth = width / columns;

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

        private void CanvasSizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            UpdateCanvas();
        }

        private void NextMonthBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _selectedMonth++;
            if (_selectedMonth > 12)
            {
                _selectedMonth = 1;
                _selectedYear++;
            }
            UpdateState();
        }

        private void PreviousMonthBtnClickEvent(object sender, RoutedEventArgs e)
        {
            _selectedMonth--;
            if(_selectedMonth < 1)
            {
                _selectedMonth = 12;
                _selectedYear--;
            }
            UpdateState();
        }

        private void RefreshBtnClickEvent(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        private void CanvasMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(Rectangle))
            {
                // block date saved in uid during the creation
                string dateStr = ((Rectangle)e.Source).Uid;
                try
                {
                    var dialog = new NewTaskWindow(_services, string.Empty, DateTime.Parse(dateStr));
                    dialog.ShowDialog();
                    UpdateState();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private Label GetTaskItem(TaskModel taskModel)
        {
            var label = new Label()
            {
                Content = (taskModel.IsDone ? "✓ " : "") + taskModel.Text
            };
            var brush = new SolidColorBrush()
            {
                Color = (Color)ColorConverter.ConvertFromString(taskModel.Project.ColorHex),
            };
            label.Background = brush;
            label.BorderThickness = new Thickness(1);
            label.Uid = taskModel.Id;
            label.MouseDown += TaskClickedEvent;
            return label;
        }
    }
}
