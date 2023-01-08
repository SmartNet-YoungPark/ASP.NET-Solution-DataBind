using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_OperationsInMVC.Models
{
    class MemberBusinessLayer  
    {
        public List<Member> GetAllMembers()
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
                SqlCommand cmd = new SqlCommand("spGetAllMembers", con);

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

        //Add employee into the database. This method takes an argument of Employee type which contains the 
        //employee that is going to stored in the database
        public void AddMember(Member member)
        {
            //Creating the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //Establishing the connection to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //Creating the command object by passing the stored procedure and connection object as argument
                //This stored procedure is used to store the employee in to the database
                SqlCommand cmd = new SqlCommand("spAddMember", con)
                {
                    //Specifying the command as stored procedure
                    CommandType = CommandType.StoredProcedure
                };

                //Creating SQL parameters because that stored procedure accept some input values
                SqlParameter paramName = new SqlParameter
                {
                    //Storing the parameter name of the stored procedure into the SQL parameter
                    //By using ParameterName property 
                    ParameterName = "@Name",
                    //storing the parameter value into sql parameter by using Value ptoperty
                    Value = member.Name
                };
                //Adding that parameter into Command objects Parameter collection by using Add method
                //which will take the SQL parameter name as argument
                cmd.Parameters.Add(paramName);

                //Same for all other parameters (Gender, City, DateOfBirth )
                SqlParameter paramGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = member.Gender
                };
                cmd.Parameters.Add(paramGender);

                SqlParameter paramCity = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = member.City
                };
                cmd.Parameters.Add(paramCity);

                SqlParameter paramSalary = new SqlParameter
                {
                    ParameterName = "@Salary",
                    Value = member.Salary
                };
                cmd.Parameters.Add(paramSalary);

                SqlParameter paramDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = member.DateOfBirth
                };
                cmd.Parameters.Add(paramDateOfBirth);

                //Open the connection and execute the command on ExecuteNonQuery method
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateMember(Member member)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdatemember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = member.ID;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = member.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@Gender";
                paramGender.Value = member.Gender;
                cmd.Parameters.Add(paramGender);

                SqlParameter paramCity = new SqlParameter();
                paramCity.ParameterName = "@City";
                paramCity.Value = member.City;
                cmd.Parameters.Add(paramCity);

                SqlParameter paramSalary = new SqlParameter();
                paramSalary.ParameterName = "@Salary";
                paramSalary.Value = member.Salary;
                cmd.Parameters.Add(paramSalary);

                SqlParameter paramDateOfBirth = new SqlParameter();
                paramDateOfBirth.ParameterName = "@DateOfBirth";
                paramDateOfBirth.Value = member.DateOfBirth;
                cmd.Parameters.Add(paramDateOfBirth);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteMember(Member member)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = member.ID;
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

