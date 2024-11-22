using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class LecturerDetailsRequest
    {
		[Required(ErrorMessage = "First name is required.")]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		[StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
		public string Email { get; set; }
	}
}
