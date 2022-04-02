using System.ComponentModel.DataAnnotations;


namespace EFCoreDAL
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        public string DeptName { get; set; }

       
    }
}
