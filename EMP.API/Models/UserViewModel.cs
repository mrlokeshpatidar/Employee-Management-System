using System.ComponentModel.DataAnnotations;

namespace EMP.API.Models
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
