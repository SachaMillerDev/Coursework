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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coursework.ViewModels; // Ensure you have this namespace

namespace Coursework
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Name == "MessageHeaderInput")
            {
                MessageHeaderPlaceholder.Visibility = Visibility.Collapsed;
            }
            else if (sender is TextBox textBox2 && textBox2.Name == "MessageBodyInput")
            {
                MessageBodyPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Name == "MessageHeaderInput" && string.IsNullOrEmpty(textBox.Text))
            {
                MessageHeaderPlaceholder.Visibility = Visibility.Visible;
            }
            else if (sender is TextBox textBox2 && textBox2.Name == "MessageBodyInput" && string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBodyPlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
