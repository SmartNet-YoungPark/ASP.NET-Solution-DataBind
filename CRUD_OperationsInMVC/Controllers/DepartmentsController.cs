using CRUD_OperationsInMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_OperationsInMVC.Controllers
{ 
   
    public class DepartmentsController : Controller
    {
       
        public ActionResult Index()
            {
            EmployeeDBEntities dbContext = new EmployeeDBEntities();
            List<Department> listDepartments = dbContext.Departments.ToList();
    //        List<Employee> listEmployee = dbContext.Employees.ToList();
            return View(listDepartments);
            }
        
    }
}