using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OperationsInMVC.Models;

namespace OperationsInMVC.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class EmployeeDBEntities : DbContext
    {
        public EmployeeDBEntities() : base("name=EmployeeDBEntities2")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
       // public virtual DbSet<member> members { get; set; }
    }
}