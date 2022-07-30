using System.ComponentModel.DataAnnotations;


namespace EMP.API.Models
{
    public class EmpDetailViewModel
    {

        [Required(ErrorMessage = "Employee Name is required")]
        public string Name { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Employee Email is required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Employee Designation is required")]
        public string Designation { get; set; }


    }
}
