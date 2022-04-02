using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDAL
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Address { get; set; }

        [Display(Name="Picture")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [ForeignKey("Department")]
        [Display(Name="Department")]
        public int DeptId { get; set; }

        public Department Department { get; set; }
    }
}