using System.ComponentModel.DataAnnotations;

namespace EMP.API.Models
{
    public class EmpSalaryViewModel
    {
        [Required]
        public int EmpId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public decimal SalaryPaid { get; set; }
    }
}
