using System;

namespace CRUD_OperationsInMVC.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
