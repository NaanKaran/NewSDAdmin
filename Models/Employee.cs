using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDAdminTool.Models
{
    public class Employee
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeId { get; set; }
        //[Required(ErrorMessage = "Enter the work desk phone number in this format (###)-###-####")]
        //// [DataType(DataType.PhoneNumber)]
        //[Phone]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string WorkPhoneDesk { get; set; }
       // [Required(ErrorMessage ="Enter the work phone mobile number in this format (###)-###-####")]
       //// [DataType(DataType.PhoneNumber)]
       // [Phone]
       // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string WorkPhoneMobile { get; set; }
        public string Name { get { return (string.IsNullOrEmpty(EmployeeId) ? "" : Firstname + " " + Lastname + " " + "(" + EmployeeId + ")"); } }


    }
    public class Employees
    {
        public List<Employee> LstEmployee { get; set; }
        public Employee CurrentEmployee { get; set; }
    }
}