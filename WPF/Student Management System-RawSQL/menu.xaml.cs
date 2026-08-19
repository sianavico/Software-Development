using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Siana
{
    /// <summary>
    /// Interaction logic for menu.xaml
    /// </summary>
    public partial class menu : Page
    {
        public menu()
        {
            InitializeComponent();
        }
        private void save_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new saving());
        }
        private void update_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new updating());
        }
        private void delete_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new deleting());
        }
        private void records_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new showing());
        }
    }
}
