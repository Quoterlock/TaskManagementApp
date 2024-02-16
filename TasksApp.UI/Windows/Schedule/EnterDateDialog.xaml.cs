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

namespace TasksApp.UI.Windows.Schedule
{
    /// <summary>
    /// Interaction logic for EnterDateDialog.xaml
    /// </summary>
    public partial class EnterDateDialog : Window
    {
        public bool isConfirmed;
        public DateOnly date;
        public EnterDateDialog(string text)
        {
            InitializeComponent();
            messageTextBlock.Text = text;
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                date = DateOnly.Parse(dateTextBox.Text);
                isConfirmed = true;
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show("Write date in a correct format (DD-MM-YYYY)");
            }
        }

    }
}
