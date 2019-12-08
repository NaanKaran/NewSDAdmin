using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SDAdminTool.Models;
using System.Data;

namespace SDAdminTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string user = ControllerContext.HttpContext.User.Identity.Name;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select EmployeeId,FirstName,LastName,NetworkAlias,WorkPhoneMobile,WorkPhoneDesk from Employee", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Employee> lstEmployees = new List<Employee>();
            while (dr.Read())
            {
                lstEmployees.Add(new Employee() { Firstname = dr[1].ToString(), Lastname = dr[2].ToString(), WorkPhoneDesk = string.Format("{0:(###)-###-####}", (dr[5] == null || dr[5].ToString() == "" ? 0 : Convert.ToInt64(dr[5].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))), EmployeeId = dr[0].ToString(), WorkPhoneMobile = string.Format("{0:(###)-###-####}",( dr[4]==null ||dr[4].ToString()==""?0: Convert.ToInt64(dr[4].ToString().Replace(".","").Replace(" ","").Replace("-", "").Replace("(", "").Replace(")", "")))) });
            }
            con.Close();
            lstEmployees.Insert(0, new Employee());
            Employees e = new Employees();
            e.lstEmployee = lstEmployees;
            //e.CurrentEmployee = lstEmployees[0];// new Employee() { EmployeeId = "", Firstname = "--Select--" }; 
            return View(e);
        }
        [HttpPost]
        public ActionResult Index(Employees employee)
        {
            Employees e = new Employees();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString);
            con.Open();
            if (!string.IsNullOrEmpty(employee.CurrentEmployee.EmployeeId))
            {
                string user = ControllerContext.HttpContext.User.Identity.Name;

               
                SqlCommand cmd = new SqlCommand("Employee_AddOrUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.CurrentEmployee.EmployeeId);
                cmd.Parameters.AddWithValue("@SourceSystem", 1);
                cmd.Parameters.AddWithValue("@UpdateOnly", 1);
                cmd.Parameters.AddWithValue("@WorkPhoneMobile", employee.CurrentEmployee.WorkPhoneMobile);
                cmd.Parameters.AddWithValue("@WorkPhoneDesk", employee.CurrentEmployee.WorkPhoneDesk);
                cmd.ExecuteNonQuery();
                e.lstEmployee = GetAllEmployee(con);
                con.Close();
                //  e.CurrentEmployee = lstEmployees[0];//new Employee() { EmployeeId = "", Firstname = "Select" };//
                ViewBag.Message = "Contact Updated successfully.";
                return View(e);
            }
            else
            {
                e.lstEmployee = GetAllEmployee(con);
                con.Close();
                return View(e);
            }
        }
        private List<Employee> GetAllEmployee(SqlConnection connection)
        {

          SqlCommand  cmd = new SqlCommand("Select EmployeeId,FirstName,LastName,NetworkAlias,WorkPhoneMobile,WorkPhoneDesk from Employee", connection);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Employee> lstEmployees = new List<Employee>();

            while (dr.Read())
            {
                lstEmployees.Add(new Employee() { Firstname = dr[1].ToString(), Lastname = dr[2].ToString(), WorkPhoneDesk = string.Format("{0:(###)-###-####}", (dr[5] == null || dr[5].ToString() == "" ? 0 : Convert.ToInt64(dr[5].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))), EmployeeId = dr[0].ToString(), WorkPhoneMobile = string.Format("{0:(###)-###-####}", (dr[4] == null || dr[4].ToString() == "" ? 0 : Convert.ToInt64(dr[4].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))) });
            }
            lstEmployees.Insert(0, new Employee());
            return lstEmployees;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}