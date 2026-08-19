using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Siana
{
    class student
    {
        //why we created a new class???
        //in the update page, after correct id got entered, we preload that student data into the boxes so the user can change anyone of them which he wants
        //but for that we need to read a complete row(one student complete data), now as the data in that row is of different datatypes
        //a list CAN store things of different datatypes BUT that design is not recommended cuz we have to remember exactly at what index what type of thing
        //is stored, so we use another way
        //we return a student object, as we studied, one object inside a student class could have different types of complete data of one student
        //one object==one student data, that's exactly what we'll do here
        //then why do not make this object inside the studentrepository class, why create a new class for it???
        //cuz studentrepository.cs is for database communication, we connect to db and make queries on it, it is not specifically a student data related thing
        //it is not restricted to not make an object out of that studentrepository class, but it is not healthy
        //we make a seperate class named student just for this type of work, we hold student data in it, just like professional softwares have many files
        //then we'll make an object out of it to perform our work (store one whole row of student data in it)
        //we'll still use studentrepository class to carry out the sql work(UPDATE & SELECT WHERE ID=..)
        //but we'll use student class to carry the data like pre-loading things into the boxes and then take the updated data back
        //student class is just the data carrier, when we put id, the id is sent to a funtion in repository where a query is executed to find relevant data
        //if found, an object is made and the data of whole row is put in it, sent back to the update page and it prefills the data, then student makes
        //the changes and then presses update, now new object ius made with the new data and sent to repository to execute update query.
        //NOT RELEVANT TO THIS, BUT WHAT ABOUT WHEN SOME ROWS OF STUDENT DATA IS NEEDED TO BE SENT LIKE IN SEARCH FILTERS, THEN WHAT???
        //then repository creates a new student object for every row and puts all the data in one list, like all students data in one list
        //OVERALL, RESPONSIBILITIES ARE DIVIDED, THE UI HANDLES USER INTERACTION, STUDNET CLASS CARRIES DATA, REPOSITORY HANDLES ALL DB OPERATIONS
        //=========================================================================================================================================
        //making the properties of this class so every object of it has these properties
        public int id { get; set; }
        public string name { get; set; }
        public int marks { get; set; }
        public string course {  get; set; }
        public string dept { get; set; }
        public string campus { get; set; }
        public string address { get; set; }
    }
}
