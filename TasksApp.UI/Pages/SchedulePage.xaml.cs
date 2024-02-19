using System.Windows;
using System.Windows.Controls;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.UI.Services;
using TasksApp.UI.Windows.Schedule;

namespace TasksApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private readonly ServicesContainer _services;

        public SchedulePage(ServicesContainer services)
        {
            InitializeComponent();
            _services = services;
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            // clear list boxes
            mondayListBox.Items.Clear();
            tuesdayListBox.Items.Clear();
            wednesdayListBox.Items.Clear();
            thursdayListBox.Items.Clear();
            fridayListBox.Items.Clear();
            saturdayListBox.Items.Clear();
            sundayListBox.Items.Clear();

            // add items
            var blocks = _services.Get<IScheduleService>().GetCurrentSchedule();
            foreach (var block in blocks)
            {
                foreach(var day in block.DaysOfWeek)
                {
                    if(day == DayOfWeek.Monday)
                        mondayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Tuesday)
                        tuesdayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Wednesday)
                        wednesdayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Thursday)
                        thursdayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Friday)
                        fridayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Saturday)
                        saturdayListBox.Items.Add(GetListItem(block));
                    if (day == DayOfWeek.Sunday)
                        sundayListBox.Items.Add(GetListItem(block));
                }
            }
        }

        private ListBoxItem GetListItem(ScheduleBlockModel block)
        {
            var item = new ListBoxItem();
            var label = new Label() 
            { 
                Content = block.Text 
                + " (" + block.StartTime.ToString() 
                + "-" + block.EndTime.ToString() 
                + ")" 
            };

            var editBtn = new Button() { Content = "edit" };
            editBtn.Uid = block.Id;
            editBtn.Width = 60;
            editBtn.Click += EditBlockEvent;

            var deleteBtn = new Button() { Content = "delete"};
            deleteBtn.Uid = block.Id;
            deleteBtn.Width = 60;
            deleteBtn.Click += DeleteBlockEvent;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            stack.Children.Add(label);
            stack.Children.Add(editBtn);
            stack.Children.Add(deleteBtn);

            item.Content = stack;

            return item;
        }

        private void DeleteBlockEvent(object sender, RoutedEventArgs e)
        {
            var blockId = ((Button)sender).Uid;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this block?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var service = _services.Get<IScheduleService>();
                service.RemoveBlock(blockId);

                var dialog = new EnterDateDialog("Enter start date to apply new Schedule");
                dialog.ShowDialog();
                if (dialog.isConfirmed)
                {
                    service.UpdateFutureTasksToScheme(dialog.date.ToDateTime(TimeOnly.MinValue));
                    LoadSchedule();
                }
            }
        }

        private void EditBlockEvent(object sender, RoutedEventArgs e)
        {
            var blockId = ((Button)sender).Uid;
            var window = new EditScheduleBlockWindow(_services, blockId);
            window.ShowDialog();
            if (window.IsModified)
            {
                var dialog = new EnterDateDialog("Enter start date to apply new Schedule");
                dialog.ShowDialog();
                if (dialog.isConfirmed)
                {
                    _services.Get<IScheduleService>().UpdateFutureTasksToScheme(dialog.date.ToDateTime(TimeOnly.MinValue));
                    LoadSchedule();
                }
            }
        }

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddScheduleBlockWindow(_services);
            window.ShowDialog();
            if(window.IsModified)
            {
                var dialog = new EnterDateDialog("Enter start date to apply new Schedule");
                dialog.ShowDialog();
                if (dialog.isConfirmed)
                {
                    _services.Get<IScheduleService>().UpdateFutureTasksToScheme(dialog.date.ToDateTime(TimeOnly.MinValue));
                    LoadSchedule();
                }
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete all blocks?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _services.Get<IScheduleService>().ClearSchedule();
                LoadSchedule();
            }
        }
    }
}
