using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpDeptApp.Models
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Address { get; set; }

        [Display(Name = "Picture")]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImageEmp { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        public string ImagePath { get; set; }
        public string DeptName { get; set; }


    }
}