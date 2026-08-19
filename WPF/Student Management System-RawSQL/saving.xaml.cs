using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class saving : Page
    {
        studentrepository repo = new studentrepository();   //we have created a new object of the studentrepository class, it'll be used for db work
        public saving()   //this is a construcor(special type of method which has the same name as the class)
        {
            InitializeComponent();
            //now we call the function
            //this function of load_cities will run and fetch the cities list, we fetch that and put it in a variable
            //we then use itemsource with the combobox name to send 'em to the combobox
            //in that class, that list had some name, but when we returned that list to here, only its materials are sent to this class, its name is left behind there, so when
            //we receive it here, we need to again give it some name WHY?? cuz in itemsource line,we need to write the list name.
            List<string> depts = repo.Load_depts(); //cuz the object named "repo" is using this method, we already learned this
            //so in one line,we're calling the function as well as putting the things returned by that function
            //in a variable named "cities"
            deptbox.ItemsSource = depts;   //general syntax= name_os_combobox.ItemsSource = name_of_list
            //=============================================================================================================
            //now loading the city list
            List<string> campus_list = repo.Load_campus();
            campusbox.ItemsSource = campus_list;
        }
        //===========================================================================================================================================================================
        //===========================================================================================================================================================================
        //------------------------------------------------------------------------SAVE BUTTON FUNCTION-------------------------------------------------------------------------------
        //===========================================================================================================================================================================
        //===========================================================================================================================================================================
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //hiding all the error messages at the very start of button click, did this cuz when we input something wrong, the error appears but if we correct it and then click save again
            // the error should vanish, in this way if we hide them all at the very start of click, iot works
            //Logic: when you hide them all at the start, then you check and unhide only those which have the error, then you correct the error and then click save again
            // the click event again hides all the error messages at the very start, after thta is starts checking and this time that error does not appear cuz we
            //corrected it
            err_marks.Visibility = Visibility.Hidden;
            err_name.Visibility = Visibility.Hidden;
            err_add.Visibility = Visibility.Hidden;
            err_course.Visibility = Visibility.Hidden;
            err_dept.Visibility = Visibility.Hidden;
            err_campus.Visibility = Visibility.Hidden;
            //we use bool to check each field, if at the end all are correct together, then a message box appears that says "saved"
            bool idgood = true;
            bool marksgood = true;
            bool namegood = true;
            bool addressgood = true;
            bool coursegood = true;
            bool deptgood = true;
            bool citygood = true;
            //now take one field like take the id field, then perform different checks on it, like check null,length good,only characters/letters..., use one bool for all the checks cuz 
            //we will be going in a order of if/else-if's , like it will check the first one, if it runs then it is setted as false and it will remain false for all the ones
            //so as we go through the checks of id, we will have an error, will fix it, then we'll again press save, but this time that error will not appear but some next check will give error
            //in this way we will reach the end by clearing all the checks


            //========================================================================================================================================================================
            //  ID CHECKS FIELD
            //========================================================================================================================================================================
            /*if (string.IsNullOrWhiteSpace(idbox.Text))             //we could've used if(idbox.Text=="") but the problem is that it only catches "", it would not catch " ","   "
            {                                                     //so it's better to use string.IsNullOrWhiteSpace()
                err_id.Text = "*Field cannot be empty";
                err_id.Visibility = Visibility.Visible;
                idgood = false;
            }
            else if (!idbox.Text.All(char.IsDigit))             //used ! cuz we want it to be other than digits, then we give the error
            {                                                  //so this conditions means, if not digits then.....
                err_id.Text = "*Only digits allowed";
                err_id.Visibility = Visibility.Visible;
                idgood = false;
            }
            else                                             //as mentioned on register, this 2 step method is good for beginners but it has a serious problem, it can't handle values outside
            {                                                //the int range, if a number say 9999999999999 is given, then the program will crash, in this one , im doing PARSE() but 
                int id = int.Parse(idbox.Text);              // in the MARKS field, ill use the TRYPARSE() one, so we'll get idea of both 
                if (id <= 1000 || id >= 10000)
                {
                    err_id.Text = "*Enter within range";        
                    err_id.Visibility = Visibility.Visible;    
                    idgood = false;                                
                }
                else          //means now everything is valid 
                {
                    err_id.Visibility = Visibility.Hidden;               //this is used cuz if user previously made a mistake and corrected it, the error message is still showing
                    idgood = true;                                       //so when he again clicks the save button, all the checks will be good this time and this else part will run
                                                                         //which will ultimately hide the error message in the blink of an eye
                }
            } */
            //========================================================================================================================================================================
            //  MARKS CHECKS FIELD
            //========================================================================================================================================================================
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
            //========================================================================================================================================================================
            //   NAME CHECKS FIELD
            //========================================================================================================================================================================
            //do the checks in this order, check if empty, then things other than letters/spaces, then length, then all is good
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
            //========================================================================================================================================================================
            //   ADDRESS CHECKS FIELD
            //========================================================================================================================================================================
            if (string.IsNullOrWhiteSpace(addbox.Text))
            {
                err_add.Text = "                         *Field cannot be empty";    //done cuz the next error takes much space due to which this one 
                err_add.Visibility = Visibility.Visible;                             //is slided to the left
                addressgood = false;
            }
            else
            {
                addressgood = true;
                string address = addbox.Text;
                for (int i = 0; i < address.Length; i++)
                {
                    if (!char.IsLetter(address[i]) && !char.IsWhiteSpace(address[i]) && !char.IsDigit(address[i]) && address[i] !=',' && address[i] != '.' && address[i] != '#' && address[i] != '/')
                    {
                        err_add.Text = "*only digits/letters/special characters allowed";
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
            //========================================================================================================================================================================
            //   COURSE CHECKS FIELD
            //========================================================================================================================================================================
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
            //========================================================================================================================================================================
            //   DEPARTMENT CHECKS FIELD
            //========================================================================================================================================================================
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
            //========================================================================================================================================================================
            //   CITY CHECKS FIELD
            //========================================================================================================================================================================
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
            //========================================================================================================================================================================
            //now we check all the bool flags if they are true together
            //========================================================================================================================================================================
            if (marksgood && namegood && addressgood && coursegood && deptgood && citygood)
            {
                //now we call the addstudents function, first for string we make sure no extra spaces are present at the start/end/middle of string
                string name = Regex.Replace(namebox.Text.Trim(), @"\s+", " ");
                string address = Regex.Replace(addbox.Text.Trim(), @"\s+", " ");       //clean the addressbox too, so the db is clear and simple
                string course = coursebox.Text;                    //both course and dept are uneditable so dont need to trim or anything
                string dept = deptbox.Text;
                string city = Regex.Replace(campusbox.Text.Trim(), @"\s+", " ");
                int marks = int.Parse(marksbox.Text.Trim());
                //now we're calling the function and also putting it equal to an int cuz the function will return an id
                int id_generated = repo.add_student(marks, name, address, course, dept, city);            //FUNCTION CALL
                MessageBox.Show($"Student Data Saved Successfully\nID Assigned: {id_generated}","SAVE");
                //after the whole thing happens, then we clear all the boxes, it is not necessary cuz we already go the menu after it but para rehne do isse
                namebox.Clear();
                marksbox.Clear();
                addbox.Clear();
                coursebox.SelectedIndex = -1;
                deptbox.SelectedIndex = -1;
                campusbox.SelectedIndex = -1;
                campusbox.Text = "";       //cuz citybox is editable so also clear text that is in it
                //now we add a code line so that the menu again appears after saving
                NavigationService.Navigate(new menu());
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