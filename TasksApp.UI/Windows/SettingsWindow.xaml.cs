using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
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
using TasksApp.BusinessLogic.Services;
using TasksApp.UI.Services;

namespace TasksApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool IsModified { get; set; }
        private ServicesContainer _services;
        public SettingsWindow(ServicesContainer services)
        {
            _services = services;
            InitializeComponent();
            IsModified = false;
            dbPathTextBox.Text = Properties.Settings.Default.databasePath;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            IsModified = true;
            var path = dbPathTextBox.Text;
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    if (fileInfo.Extension == ".db")
                    {
                        Properties.Settings.Default.databasePath = path;
                        Properties.Settings.Default.Save();
                        IsModified = true;
                        MessageBox.Show("Saved");
                        return;
                    }
                }
            }
            MessageBox.Show("Select correct db path");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SelectFolderWindow("Select folder to export");
            dialog.ShowDialog();
            if(dialog.SelectedDirectory != string.Empty)
            {
                ITasksExporter exporter = new MarkDownExporter(
                    _services.Get<ICategoriesService>(),
                    _services.Get<ITasksPresenter>(),
                    _services.Get<IProjectsService>());   
                exporter.Export(dialog.SelectedDirectory);
            }
        }
    }
}
