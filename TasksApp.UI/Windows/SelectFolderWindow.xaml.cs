using System;
using System.Collections.Generic;
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

namespace TasksApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for SelectFolderWindow.xaml
    /// </summary>
    public partial class SelectFolderWindow : Window
    {
        public string SelectedDirectory {  get; set; }
        public SelectFolderWindow(string text)
        {
            InitializeComponent();
            messageLabel.Content = text;
            SelectedDirectory = string.Empty;
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(textBox.Text))
            {
                SelectedDirectory = textBox.Text;
                this.Close();
            }
            else MessageBox.Show("Select correct directory");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
