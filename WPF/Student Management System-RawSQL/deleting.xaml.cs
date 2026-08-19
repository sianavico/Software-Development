using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for deleting.xaml
    /// </summary>
    public partial class deleting : Page
    {
        studentrepository repo = new studentrepository();
        public deleting()
        {
            InitializeComponent();
            //hide the error-box
            err_id.Visibility = Visibility.Hidden;
            detailsbox.Visibility= Visibility.Hidden;
        }
        public void delete_click(object sender ,RoutedEventArgs e)
        {
            err_id.Foreground = Brushes.Red;
            idbox.BorderBrush = Brushes.Black;
            if (String.IsNullOrWhiteSpace(idbox.Text))
            {
                err_id.Text = "*Field Cannot be empty";
                err_id.Visibility = Visibility.Visible;
                idbox.BorderBrush = Brushes.Red;
            }
            else if (!int.TryParse(idbox.Text,out int id))
            {
                err_id.Text = "*Invalid Input";
                err_id.Visibility = Visibility.Visible;
                idbox.BorderBrush = Brushes.Red;
            }
            else
            {
                if(id<1001||id>9999)
                {
                    err_id.Text = "*Enter within range";
                    err_id.Visibility = Visibility.Visible;
                    idbox.BorderBrush = Brushes.Red;
                }
                else
                {
                    err_id.Visibility = Visibility.Hidden;
                    //now if the id is found, then we do things at the same time, we fill the details box and also show the message box
                    student student_data_to_delete = repo.get_student_data_by_id(id); //using the same method but making a new reference object here
                    if (student_data_to_delete != null)
                    {
                        err_id.Text = "*ID Found";
                        err_id.Visibility = Visibility.Visible;
                        err_id.Foreground = Brushes.Green;
                        idbox.BorderBrush = Brushes.Green;
                        //now we fill the detailbox and make it visible too
                        detailsbox.Visibility= Visibility.Visible;
                        detailsbox.Text = $"Student details\n     ID: {student_data_to_delete.id}\n     Name: {student_data_to_delete.name}";
                        //also we show the message box to confirm delete
                        MessageBoxResult result = MessageBox.Show("Do you really want to delete this student?", "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                             //now we send the id and delete that row, then when deleted show message that data deleted
                             bool deleted = repo.delete_student(id);
                             if (deleted)
                             {
                                 MessageBox.Show($"ID: {id} data deleted successfully");
                                 NavigationService.Navigate(new menu());
                             }
                        }
                    }    
                    else      //means id is not found, so we show error
                    {
                        err_id.Text = "*ID not found";
                        err_id.Visibility = Visibility.Visible;
                        idbox.BorderBrush= Brushes.Red;
                    }
                }
            }
        }
    }
}
