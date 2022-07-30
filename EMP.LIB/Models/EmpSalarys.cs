using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.LIB.Models
{
    public class EmpSalarys
    {
        [Key]
        public int Id { get; set; }

        public int EmpId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal SalaryPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
