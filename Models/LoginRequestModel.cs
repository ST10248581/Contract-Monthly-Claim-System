using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
