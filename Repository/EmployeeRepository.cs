using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SDAdminTool.Models;

namespace SDAdminTool.Repository
{
    public class EmployeeRepository
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["EmployeeConnection"].ConnectionString;
        public Employees GetEmployees()
        {

            //Testing purpose
            return new Employees()
            {
                LstEmployee = new List<Employee>() {
                new Employee(){ },
                new Employee() { EmployeeId = "hello", Firstname = "Test", Lastname = "Test1", WorkPhoneDesk = "(000)-123 6543", WorkPhoneMobile = "(000)-123 6544" },
                new Employee() { EmployeeId = "hello2", Firstname = "Test", Lastname = "Test2", WorkPhoneDesk = "(000)-123 6543", WorkPhoneMobile = "(000)-123 6544" },

            }
            };

            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select EmployeeId,FirstName,LastName,NetworkAlias,WorkPhoneMobile,WorkPhoneDesk from Employee", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Employee> lstEmployees = new List<Employee>();
            while (dr.Read())
            {
                lstEmployees.Add(new Employee() { Firstname = dr[1].ToString(), Lastname = dr[2].ToString(), WorkPhoneDesk = string.Format("{0:(###)-###-####}", (dr[5] == null || dr[5].ToString() == "" ? 0 : Convert.ToInt64(dr[5].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))), EmployeeId = dr[0].ToString(), WorkPhoneMobile = string.Format("{0:(###)-###-####}", (dr[4] == null || dr[4].ToString() == "" ? 0 : Convert.ToInt64(dr[4].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))) });
            }
            con.Close();
            lstEmployees.Insert(0, new Employee());
            Employees e = new Employees();
            e.LstEmployee = lstEmployees;

            return e;
        }

        public void AddOrUpdate(Employees employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("Employee_AddOrUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", employee.CurrentEmployee.EmployeeId);
            cmd.Parameters.AddWithValue("@SourceSystem", 1);
            cmd.Parameters.AddWithValue("@UpdateOnly", 1);
            cmd.Parameters.AddWithValue("@WorkPhoneMobile", employee.CurrentEmployee.WorkPhoneMobile);
            cmd.Parameters.AddWithValue("@WorkPhoneDesk", employee.CurrentEmployee.WorkPhoneDesk);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}