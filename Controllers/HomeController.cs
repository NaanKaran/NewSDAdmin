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
        //[Authorize(Users=@"userrole\username")]

        [Authorize]
        public ActionResult Index(string autherize)
        {
            ViewBag.Autherize = autherize;
            return View();
        }

        
        [HttpGet]
        public ActionResult GetEmployeeDetails()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", new { autherize  = "Your are Not Autherize."});
            }
            var employees = new EmployeeRepository().GetEmployees();
            return View(employees);
        }
        [HttpPost]
        public ActionResult GetEmployeeDetails(Employees employee)
        {
          
            if (!string.IsNullOrEmpty(employee.CurrentEmployee.EmployeeId))
            {
                new EmployeeRepository().AddOrUpdate(employee);
                ViewBag.Message = "Contact Updated successfully.";
               
            }
            var employees = new EmployeeRepository().GetEmployees();
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