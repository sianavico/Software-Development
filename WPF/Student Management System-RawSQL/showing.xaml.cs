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
    /// Interaction logic for showing.xaml
    /// </summary>
    public partial class showing : Page
    {
        studentrepository repo = new studentrepository();
        public showing()
        {
            InitializeComponent();
            show_all_records();
        }
        public void show_all_records()
        {
            List<student> all_students_list= repo.load_all_records();      //all_students_list is just reference variable name for the list that comes to us
            //now we received the list, we just bind it to the datagrid, it will connect it to the colimns auto
            records_grid.ItemsSource = all_students_list;
        }
        public void back_button(object sender,RoutedEventArgs e)
        {
            NavigationService.Navigate(new menu());
        }
    }
}
