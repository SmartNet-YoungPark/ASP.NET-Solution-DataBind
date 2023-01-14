using CRUD_OperationsInMVC;
using CRUD_OperationsInMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRUD_OperationsInMVC.Controllers
{
   
  
    public class EmployeesController : Controller
    {
        public ActionResult Index(int departmentId)
        {
            EmployeeDBEntities dbContext = new EmployeeDBEntities();
            List<Employee> employees = dbContext.Employees.Where(emp => emp.DepartmentId == departmentId).ToList();
             return View(employees);
        }


        public ActionResult Details(int id)
        {
            EmployeeDBEntities dbContext = new EmployeeDBEntities();
            Employee employee = dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            return View(employee);
        }
    }
}