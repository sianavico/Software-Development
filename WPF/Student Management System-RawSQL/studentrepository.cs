using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Siana
{
    class studentrepository
    {
        //instead of connecting to the db inside every method, do it just once INSIDE THE CLASS
        SqlConnection con = new SqlConnection("Server=ASADPC;Database=flex;Trusted_Connection=True;TrustServerCertificate=True;");
        //===============================================================================================================================
        //now we're creating a function of type 'list', we'll get the data from dept table and then we'll make a list of it here and then send it to the mainwindow function 
        //in the other file, from where it will be send to the combobox at the time when program starts
        //===============================================================================================================================================
        //-------------------------------------------------------DEPARTMENTS LOADING FROM DB METHOD------------------------------------------------------
        //===============================================================================================================================================
        public List<string> Load_depts()
        {
            con.Open();
            string query = "SELECT dept_name FROM depts";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> dept_list= new List<string>();                                                                                                                        
            while (reader.Read())
            {        //GetString will read just one city, so what we can do is read that city, put it in a temp variable and then put it in the list
                     //though we can also directly put it like devs do, but for starters we're using the temp variable method
                     string temp = reader.GetString(0);
                     dept_list.Add(temp);
            }
            reader.Close();
            con.Close();
            return dept_list;
        }
        //==================================================================================================================================================
        //----------------------------------------------------------CITIES LOADING FROM DB METHOD-----------------------------------------------------------
        //==================================================================================================================================================
        public List<string> Load_campus()
        {
            con.Open();
            string query = "SELECT campus_name FROM campus";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader=cmd.ExecuteReader();
            List<string> campus_list = new List<string>();                 //make a new list, just like we make a new variable before putting something in it
            while(reader.Read())
            {
                string temp = reader.GetString(0);
                campus_list.Add(temp);
            }
            reader.Close();
            con.Close();
            return campus_list;
        }
        //===============================================================================================================================================
        //--------------------------------------------------------------SAVE STUDENT DATA QUERY----------------------------------------------------------
        //===============================================================================================================================================
        public int add_student(int marks,string name, string address,string course,string dept,string campus)//making it int cuz we want to return the newly generated id when student data is saved
        {
            con.Open();
            string query = "INSERT INTO student_data(marks,student_name,student_address,course,dept,campus) VALUES(@marks,@name,@address,@course,@dept,@campus) SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@marks", marks);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@course", course);
            cmd.Parameters.AddWithValue("@dept", dept);
            cmd.Parameters.AddWithValue("@campus", campus);
            int id_generated = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return id_generated;
        }
        //===============================================================================================================================================
        //----------------------------------------------------------PRELOADING STUDENT DATA IN UPDATE PAGE-----------------------------------------------
        //===============================================================================================================================================
        public student get_student_data_by_id(int id)
        {
            con.Open();
            string query = "SELECT * FROM student_data WHERE id=@id";   //* will select the whole row and reader will get it
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.Read())
            {
                reader.Close();                //remember to close the connection in both if & else blocks, otherwise it crashes, speaking from experience
                con.Close();
                return null;
            }
            else  //means record found, now we create a student object, put all the data from that row into it and then return it
            {
                //create student object
                //fill it from reader
                //return student object
                student student_data_to_preload = new student();  //new object of student class
                //now we read, no need for while loop cuz there will be one row
                //understand this, there is one row but many columns each have its own datatype so we have to put them seperatly(like use seperate lines)
                //we tell it to look into a certain named column and put its thing into the property,we'll use the object name with it
                student_data_to_preload.id = int.Parse(reader["id"].ToString());  //even if we don't use id, we still put it in object so that complete data is available for a student
                student_data_to_preload.name = reader["student_name"].ToString();
                student_data_to_preload.marks = int.Parse(reader["marks"].ToString());
                student_data_to_preload.course = reader["course"].ToString(); 
                student_data_to_preload.dept = reader["dept"].ToString();
                student_data_to_preload.campus = reader["campus"].ToString();
                student_data_to_preload.address = reader["student_address"].ToString();
                reader.Close();
                con.Close();
                return student_data_to_preload;
            }
        }
        //===============================================================================================================================================
        //----------------------------------------------------------UPDATE STUDENT DATA QUERY------------------------------------------------------------
        //===============================================================================================================================================
        public bool update_student(int id,string name,int marks,string course,string dept,string campus,string address)
        {
            con.Open();                                  //NOTE: the @ sign at the start of query lets us write on multiple lines to look cleaner
            string query = @"UPDATE student_data                           
                            SET 
                                student_name=@name,
                                marks=@marks,
                                course=@course,
                                dept=@dept,
                                campus=@campus,
                                student_address=@address
                            WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@name",name);
            cmd.Parameters.AddWithValue("@marks",marks);
            cmd.Parameters.AddWithValue("@course",course);
            cmd.Parameters.AddWithValue("@dept", dept);
            cmd.Parameters.AddWithValue("@campus", campus);
            cmd.Parameters.AddWithValue("@address", address);
            int rows_effected = cmd.ExecuteNonQuery();        //ExecuteNonQuery always return an int(the number of rows effected)
            con.Close();
            if(rows_effected==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //===============================================================================================================================================
        //----------------------------------------------------------SEARCH ID IN DB FOR DELETE QUERY-----------------------------------------------------
        //===============================================================================================================================================
        public bool search_id(int id)
        {
            con.Open();
            string query = "SELECT * FROM student_data Where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",id);
            //now we use execute reader, to check if something is returned, executenonquery is used when inserting/updating/deleting data
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()==true)
            {
                reader.Close();
                con.Close();
                return true;
            }
            else
            {
                reader.Close();
                con.Close();
                return false;
            }
        }
        //===============================================================================================================================================
        //-------------------------------------------------------------------DELETE QUERY----------------------------------------------------------------
        //===============================================================================================================================================
        public bool delete_student(int id)
        {
            con.Open();
            string query = "DELETE FROM student_data WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", id);
            int rows_effected = cmd.ExecuteNonQuery();
            if(rows_effected>0)
            {
                return true;
            }
            else
            {
                 return false;
            }
            
        }
        //===============================================================================================================================================
        //-------------------------------------------------------------------LOAD ALL STUDENTS RECORDS---------------------------------------------------
        //===============================================================================================================================================
        public List<student> load_all_records()
        {
            con.Open();
            string query = "SELECT * FROM student_data";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            //use a while loop, for each row,create a new object
            List<student> all_students_list = new List<student>();
            while(reader.Read())
            {
                //now first create a student object, you'll be thinking, how to make different names for differnt objects, the answer is we dont, cuz we
                //just put them into the datagrid, we dont need to perform operations on them like we need this particiular object or this one, so we dont
                //need to label them, we just use one variable name, and that is used each time just to refer to the newly created object, we use any name
                student s = new student();
                //now we put the things into their respective properties of the object , e.g the property was id, so we read id from row 0 and put it in it
                s.id = reader.GetInt32(0);
                s.name = reader.GetString(2);          //in db, row 1 is marks and row 2 is name, but here im doing name first
                s.marks = reader.GetInt32(1);
                s.course = reader.GetString(4);
                s.dept = reader.GetString(5);
                s.campus = reader.GetString(6);
                s.address = reader.GetString(3);
                //now as this object's properties are all filled, we put this object in the list
                //so we first filled the object, then we filled the list with the object one by one
                all_students_list.Add(s);
            }
            //now the loop has ended, we close the reader and connection , then return the list
            reader.Close();
            con.Close();
            return all_students_list;
        }
    }
}
