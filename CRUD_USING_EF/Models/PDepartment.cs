using System.ComponentModel.DataAnnotations;

namespace CRUD_USING_EF.Models
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    {
    }

    public class DepartmentMetaData
    {
        [Display(Name = "Department Name")]
        public string Name { get; set; }
    }
}