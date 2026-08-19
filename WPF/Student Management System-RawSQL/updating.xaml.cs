using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;
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
    public partial class updating : Page
    {
        studentrepository repo = new studentrepository();   //each window must have its own object, but all the objects talk to the same db
        public updating()
        {
            InitializeComponent();
            //------------------------------------------------------------------------------------------------------------------------------
            //we also load the citybox and dept box here, same code just like we did in the constructor of saving file,we just call the function
            //get the loaded list, and send it to the respective comboboxes
            List<string> depts = repo.Load_depts(); 
            deptbox.ItemsSource = depts;
            List<string> city_list = repo.Load_campus();
            campusbox.ItemsSource = city_list;
            //-------------------------------------------------------
            //hide all the boxes when the program starts, just call the hideboxes function
            hideboxes();
        }
        //========================================================================================================================================
        //========================================================================================================================================
        //==============================================================SHOW BOXES================================================================
        public void showboxes()
        {
            namelabel.Visibility = Visibility.Visible;
            namebox.Visibility = Visibility.Visible;
            markslabel.Visibility = Visibility.Visible;
            marksbox.Visibility = Visibility.Visible;
            courselabel.Visibility = Visibility.Visible;
            coursebox.Visibility = Visibility.Visible;
            deptlabel.Visibility = Visibility.Visible;
            deptbox.Visibility = Visibility.Visible;
            campuslabel.Visibility = Visibility.Visible;
            campusbox.Visibility = Visibility.Visible;
            addresslabel.Visibility = Visibility.Visible;
            addressbox.Visibility = Visibility.Visible;
            update_button.Visibility = Visibility.Visible;
        }
        //========================================================================================================================================
        //========================================================================================================================================
        //==============================================================HIDE BOXES================================================================
        public void hideboxes()
        {
            namelabel.Visibility = Visibility.Hidden;
            namebox.Visibility = Visibility.Hidden;
            markslabel.Visibility = Visibility.Hidden;
            marksbox.Visibility = Visibility.Hidden;
            courselabel.Visibility = Visibility.Hidden;
            coursebox.Visibility = Visibility.Hidden;
            deptlabel.Visibility = Visibility.Hidden;
            deptbox.Visibility = Visibility.Hidden;
            campuslabel.Visibility = Visibility.Hidden;
            campusbox.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            addressbox.Visibility = Visibility.Hidden;
            update_button.Visibility = Visibility.Hidden;
        }
        //========================================================================================================================================
        //========================================================================================================================================
        //==========================================KEYDOWN EVENT TO PRESS ENTER FROM KEYBOARD WHEN INSIDE THE IDBOX==============================
        //========================================================================================================================================
        //========================================================================================================================================
        private void idbox_keydown(object sender, KeyEventArgs e)
        {                                        //when we're in the idbox, and we press anything, this event fires, but when the pressed key pressed
            if(e.Key==Key.Enter)                 //is ENTER, then the id_search event is fired from this event, in other words the search button is
            {                                    //pressed when we pressed ENTER when in the idbox
                id_search(sender, e);
            }
        }
        //========================================================================================================================================
        //========================================================================================================================================
        //========================================================================================================================================
        //==========================================SEARCHING ID IN DATABASE AND ID-BOX CHECKS====================================================
        //========================================================================================================================================
        //========================================================================================================================================
        public void id_search(object sender, RoutedEventArgs e)
        {
            err_id.Visibility = Visibility.Hidden;
            err_id.Foreground = Brushes.Red;  //cuz after we set them to green after a correct id, they stay green unless we change em
            idbox.BorderBrush = Brushes.Black; //we make it black for every search, then according to search, it converts to green/red
            //================now checks=====================
            if (string.IsNullOrWhiteSpace(idbox.Text))
            {
                err_id.Text = "*Cannot be empty";
                err_id.Visibility = Visibility.Visible;
                idbox.BorderBrush = Brushes.Red;
                hideboxes();
            }
            else if (!int.TryParse(idbox.Text, out int id))    //we didn't do char(is digit) type shit here, we used TryParse cuz it can also handle
            {                                                 //numbers that are out of int range like 9999999999
                err_id.Text = "*Invalid Input";
                err_id.Visibility = Visibility.Visible;
                idbox.BorderBrush = Brushes.Red;
                hideboxes();
            }
            else  //no need to put text in a int variable, it is already done in TryParse
            {
                if(id<1001||id>9999)
                {
                    err_id.Text = "*Enter within range";
                    err_id.Visibility = Visibility.Visible;
                    idbox.BorderBrush = Brushes.Red;
                    hideboxes();        //we hide them in all error blocks cuz after a correct id match they appear, but after it if we enter incorrect, then they may be visible still
                }
                else   //all good now
                {
                    //now as every input check is good, now we send that id to db and check it there if it really exists there or not, we call it from 
                    //we call that function of updating from this else block cuz everything is good coming into this block.
                    //------------------------------------------------------------------------------------------------------------
                    student student_data_to_preload = repo.get_student_data_by_id(id); //student_data_to_preload is a reference to retrive the data that
                                                                                       //will be returned from repository,just like another rider that will
                                                                                       //carry the same package but in another city(file)
                    if(student_data_to_preload!=null)
                    {
                        //then we show a green message in the id error box to show that id found, we may also turn the idbox border green
                        //we also unhide all the boxes
                        err_id.Text = "*ID Found";
                        err_id.Foreground = Brushes.Green;
                        err_id.Visibility = Visibility.Visible;
                        idbox.BorderBrush = Brushes.Green;
                        //now unhide the boxes too
                        showboxes();
                        //now we fill the boxes from the object
                        namebox.Text = student_data_to_preload.name;
                        marksbox.Text = student_data_to_preload.marks.ToString();
                        coursebox.Text = student_data_to_preload.course;         
                        deptbox.Text = student_data_to_preload.dept;       
                        campusbox.SelectedItem = student_data_to_preload.campus;        
                        addressbox.Text = student_data_to_preload.address;
                    }
                    else
                    {
                        err_id.Foreground = Brushes.Red;
                        err_id.Text = "*ID Not Found";
                        err_id.Visibility = Visibility.Visible;
                        idbox.BorderBrush = Brushes.Red;
                        //here hide the boxes again, cuz if you first search a correct id, boxes appear, but then if you enter an incorrect id, then 
                        //the boxes may still be visible
                        hideboxes();
                    }
                }
            }
        }
        //========================================================================================================================================
        //========================================================================================================================================
        //========================================================================================================================================
        //==========================================UPDATE BUTTON & BOXES ERROR CHECKS AT TIME OF CLICK===========================================
        //========================================================================================================================================
        //========================================================================================================================================
        private void update_click(object sender, RoutedEventArgs e)
        {
            //now just like saving.xaml.cs
            //first hide all the errors
            //then make bools for validation
            //then validate each thing (name,marks,city,....), give their erros and set their bool
            //if everything is valid, send the things to execute the update WHERE id=@id query, and then show the message 'updated successfully'
            err_marks.Visibility = Visibility.Hidden;
            err_name.Visibility = Visibility.Hidden;
            err_add.Visibility = Visibility.Hidden;
            err_course.Visibility = Visibility.Hidden;
            err_dept.Visibility = Visibility.Hidden;
            err_campus.Visibility = Visibility.Hidden;
            //now bools
            bool marksgood = true;
            bool namegood = true;
            bool addressgood = true;
            bool coursegood = true;
            bool deptgood = true;
            bool citygood = true;
            //now the fields check
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------NAME FIELD CHECK--------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(namebox.Text))
            {
                err_name.Text = "*Field cannot be empty";
                err_name.Visibility = Visibility.Visible;
                namegood = false;
            }
            else
            {                                         //why we set namegood to true at the start of this else??in first run, if we enter asad123, then error comes and namegood is set to false,
                                                      //then we correct it and then click save again,that error doesnt come again but namegood is still false, so we set it to true at the
                namegood = true;                      //start of this else condition, cuz then length is checked, if there was character error and namegood=false, then it would stop the
                foreach (char c in namebox.Text)       //length check to run, this is useful otherwsie there was a chance two errors would come(letter error and length error), so we only
                {                                     //move to the next error if the previous one passed
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        err_name.Text = "*Only letters allowed";
                        err_name.Visibility = Visibility.Visible;
                        namegood = false;
                        break;
                    }
                }
                string name = namebox.Text;
                if (namegood)
                {
                    if (name.Length < 3)
                    {
                        err_name.Text = "*Name too short";
                        err_name.Visibility = Visibility.Visible;
                        namegood = false;
                    }
                    else                                                                 //ALL ISS WELL NOW
                    {
                        err_name.Visibility = Visibility.Hidden;
                        namegood = true;
                    }                                                                      //========================================================================================
                }                                                                          //in the namebox and address field, i used different ways to the same thing so that we know 
            }                                                                               //different approaches we can use to the same idea
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------MARKS FIELD CHECK-------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(marksbox.Text))
            {
                err_marks.Text = "*Field cannot be empty";
                err_marks.Visibility = Visibility.Visible;
                marksgood = false;
            }
            else if (!int.TryParse(marksbox.Text, out int marks))  //this checks if entered thing is anything but a number like 12A, @12, it will return false in these cases
            {                                                    //and the errro message will appear
                err_marks.Text = "*Invalid Input";
                err_marks.Visibility = Visibility.Visible;
                marksgood = false;
            }
            else                                            //this means the number entered was a valid integer but we still don't know if it is in the range or not
            {                                               //so now we have to check if it is within the range or not
                if (marks < 0 || marks > 100)
                {
                    err_marks.Text = "Enter within range";
                    err_marks.Visibility = Visibility.Visible;
                    marksgood = false;
                }
                else                                             //MEANS ALL IS WELL NOW
                {
                    err_marks.Visibility = Visibility.Hidden;
                    marksgood = true;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------COURSE FIELD CHECK------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            //now this course combobox is uneditable so we only have to show one error message to tell them to select some field, so we use selecteditem/selectedindex,either one is good
            if (coursebox.SelectedIndex == -1)
            {
                err_course.Text = "*Field required";
                err_course.Visibility = Visibility.Visible;
                coursegood = false;
            }
            else
            {
                err_course.Visibility = Visibility.Hidden;
                coursegood = true;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------DEPT FIELD CHECK--------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            //same like course box
            if (deptbox.SelectedIndex == -1)
            {
                err_dept.Text = "*Field required";
                err_dept.Visibility = Visibility.Visible;
                deptgood = false;
            }
            else
            {
                err_dept.Visibility = Visibility.Hidden;
                deptgood = true;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------CITY FIELD CHECK--------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (campusbox.SelectedItem == null)             //only this check is done here, this will give error if the combobox is left unattended and also if we wrote something other than
            {                                          //things from the list, all other checks like if !letters are done in the KeyUp method
                err_campus.Text = "*Select valid city";
                err_campus.Visibility = Visibility.Visible;         
                citygood = false;                                      
            }                                                    
            else                                                       
            {                                                           
                err_campus.Visibility = Visibility.Hidden;
                citygood = true;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------ADDRESS FIELD CHECK-----------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(addressbox.Text))
            {
                err_add.Text = "*Field cannot be empty";
                err_add.Visibility = Visibility.Visible;
                addressgood = false;
            }
            else
            {
                addressgood = true;
                string address = addressbox.Text;
                for (int i = 0; i < address.Length; i++)
                {
                    if (!char.IsLetter(address[i]) && !char.IsWhiteSpace(address[i]) && !char.IsDigit(address[i]))
                    {
                        err_add.Text = "*only digits/letters allowed";
                        err_add.Visibility = Visibility.Visible;
                        addressgood = false;
                        break;
                    }
                }
                if (addressgood)     //means all is well
                {
                    err_add.Visibility = Visibility.Hidden;
                    addressgood = true;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------NOW CHECK ALL BOOLS-----------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (marksgood && namegood && addressgood && coursegood && deptgood && citygood)
            {
                //now first check for extra spaces and remove them using Trim() and Regex.Replace and also we put them in variables so we can send them
                string name = Regex.Replace(namebox.Text.Trim(), @"\s+", " ");
                int marks = int.Parse(marksbox.Text.Trim());               //did casting cuz in marksbox.Text, everything is like a string
                string course = coursebox.Text;
                string dept = deptbox.Text;
                string city = Regex.Replace(campusbox.Text.Trim(), @"\s+", " ");
                string address = Regex.Replace(addressbox.Text.Trim(), @"\s+", " ");    //clean the address too, to maintain a clean database
                int id = int.Parse(idbox.Text);   //cuz we have to send it via function, so we put it in a variable by casting it
                //now we call the function and send these things as well as the id to them cuz it needs to know the id to update it at that id place
                bool updated = repo.update_student(id, name, marks, course, dept, city, address);
                if (updated)
                {
                    MessageBox.Show("Data Updated Successfully","UPDATE");
                    //after whole thing happens, just return to the menu, no need to clear the boxes,cuz we already going back to the menu
                    NavigationService.Navigate(new menu());
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }
            }
        }
        //===========================================================================================================================================================================
        //===========================================================================================================================================================================
        //-----------------------------------------------------CITY BOX LIVE FILTERING & LIVE INVALID INPUT CHECKING FUNCTIONS------------------------------------------------------
        //===========================================================================================================================================================================
        //===========================================================================================================================================================================
        private void campusbox_livefiltering(object sender, KeyEventArgs e)  //making it int cuz we want to return the newly generated id when student data is saved
        {
            //first we creat a reference of that inner textbox
            TextBox innertextbox = (TextBox)campusbox.Template.FindName("PART_EditableTextBox", campusbox);
            string typed = campusbox.Text;
            //so first fetch the list of cities that was returned from studentrepository.cs, that is also being used in mainwindow
            List<string> city_list = repo.Load_campus();
            //make a new list for filtered results
            List<string> filtered_cities = new List<String>();
            //now we make a for loop, that will run to the end of the whole list using count as end condition, it is a property that tells the amount of things in the list
            for (int i = 0; i < city_list.Count; i++)
            {
                if (city_list[i].StartsWith(typed, StringComparison.OrdinalIgnoreCase))
                {
                    filtered_cities.Add(city_list[i]);
                }
            }
            campusbox.ItemsSource = filtered_cities;  //itemsource only changes/filters the items inside the list, it does not open the dropdown for us automatically
            campusbox.IsDropDownOpen = true;          //for that we use this line which opens the filtered dropdown for us so we can immediately see the matches
                                                    //-----------------------------------------------------------------------------------------------------------------------
            campusbox.Text = typed;         //the second line of this method and this line are used together, they are used so that when we are writing something, it does'nt auto select the match.
                                          //in the first line of method, we put the text so far written in a variable, and then this line is used cuz after itemsource, WPF might select a match
                                          //while we are not fully complete writing, so to not make it do that what we do, is we put the exact text that we had written again in the citybox
                                          //we say "no WPF, put back exactly what the user typed", so we can edit naturally
                                          //----------------------------------------------------------------------------------------------------------------------------------------
                                          //there is still a problem of cursor, when we select something, and then backspace, instead of cursor being on the 2nd last character, it goes
                                          //to the start, if we do backspace again, it again goes to the very start, it is annoying, to solve that, we have to work with the inner textbox
                                          //of the citybox, there is a solution to this cursor problem ".CaretIndex" but that belongs to the inner textbox, so we have to first learn how to
                                          //access the inner textbox of a combobox using reference   DONE ON REGISTER
            innertextbox.CaretIndex = innertextbox.Text.Length;  //now this will move the cursor to the end of text everytime text changes,problem solved
            //now to remove the blue highlighted thingy when we select something from the list,we dont want it to make it blue we just want it to put it from the list to the upper textbox
            //for that we will also use that reference we made
            innertextbox.SelectionLength = 0;           //it will select nothing , thus removing the blue problem, but it still has one thing
                                                        //it works when we write something and then select from the filtered result, but if we don't write something and select directly 
                                                        //from the list, it still highlights blue the selected text, to solve that, we need to make a new event named SelectionChanged
                                                        //in it we do the same cleanup of .CaretIndex and .SelectionLength, im not doing it, but know this simple process
        }
        //this below function will check if the pressed thing is a letter or not, if not it'll ignore it as well as give a error 'bout it
        private void campusbox_inputcheck(object sender, TextCompositionEventArgs e)
        {
            //in this we intercept a what is pressed before it enters the textbox, if not a letter, we dont let it inside the box and also show a error
            //as e.text is a string so we can't do char methods on it unless we take the character from that string.
            //for this event, even though we still get a single character each time but still e.text has it in form of a string, so we have to extract it and then use char.IsLetter on it
            //in that string, it will be the first and only character each time, so just use bracket indexing to get it
            char temp = e.Text[0];
            if (!char.IsLetter(temp))
            {
                err_campus.Text = "*Only letters allowed";
                err_campus.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else
            {
                err_campus.Visibility = Visibility.Hidden;
                e.Handled = false;
            }
        }
    }
}
