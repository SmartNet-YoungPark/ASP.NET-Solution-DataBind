using System;

namespace CRUD_OperationsInMVC.Models
{
    //No Name Interface
    internal interface IMember
    {
        int ID { get; set; }
        string Gender { get; set; }
        string City { get; set; }
        decimal Salary { get; set; }
        DateTime DateOfBirth { get; set; }
    }
    public class Member : IMember
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
