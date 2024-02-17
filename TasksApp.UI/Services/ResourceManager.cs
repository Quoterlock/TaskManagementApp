using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TasksApp.UI.Services
{
    public static class ResourceManager
    {
        public static Image GetIcon(string key)
        {
            return new Image()
            {
                Source = (BitmapImage)Application.Current.Resources[key],
            };
        }

        public static SolidColorBrush GetColor(string key)
        {
            return (SolidColorBrush)Application.Current.Resources[key];
        }
    }
}
