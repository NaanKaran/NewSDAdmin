using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SDAdminTool.Models;
using System.Data;
using SDAdminTool.Repository;

namespace SDAdminTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string user = ControllerContext.HttpContext.User.Identity.Name;
            var employees = new EmployeeRepository().GetEmployees();
            string vUserName = Environment.UserName;
            ViewBag.UserName = vUserName;
            return View(employees);
        }
        [HttpPost]
        public ActionResult Index(Employees employee)
        {
            var employees = new EmployeeRepository().GetEmployees();
            if (!string.IsNullOrEmpty(employee.CurrentEmployee.EmployeeId))
            {
                string user = ControllerContext.HttpContext.User.Identity.Name;
                ViewBag.Message = "Contact Updated successfully.";
               
            }
            return View(employees);
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