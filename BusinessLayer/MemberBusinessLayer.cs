using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BusinessLayer
{
    class MemberBusinessLayer  
    {
        public List<Member> GetAllEmployess()
        {
            //Reads the connection string from web.config file. The connection string name is DBCS
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            //Create List of employees collection object which can store list of employees
            List<Member> members = new List<Member>();

            //Establish the Connection to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //Creating the command object by passing the stored procedure that is used to
                //retrieve all the employess from the tblEmployee table and the connection object
                //on which the stored procedure is going to execute
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);

                //Specify the command type as stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                //Open the connection
                con.Open();

                //Execute the command and stored the result in Data Reader as the method ExecuteReader
                //is going to return a Data Reader result set
                SqlDataReader rdr = cmd.ExecuteReader();

                //Read each employee from the SQL Data Reader and stored in employee object
                while (rdr.Read())
                {
                    //Creating the employee object to store mem information
                    Member mem = new Member();
                    mem.ID = Convert.ToInt32(rdr["Id"]);
                    mem.Name = rdr["Name"].ToString();
                    mem.Gender = rdr["Gender"].ToString();
                    mem.City = rdr["City"].ToString();
                    mem.Salary = Convert.ToDecimal(rdr["Salary"]);
                    mem.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);

                    //Adding that employee into List of employees collection object
                    members.Add(mem);
                }
            }
            //Return the list of members that is stored in the list collection of employees
            return members;
        }

    }
}

