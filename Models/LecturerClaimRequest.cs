using System.ComponentModel.DataAnnotations;


namespace CMCS.Models
{
	public class LecturerClaimRequest
	{

		[Required(ErrorMessage = "Please enter hours worked.")]
		[Range(1, 24, ErrorMessage = "Hours worked must be between 1 and 24.")]
		public int HoursWorked { get; set; }

		[Required(ErrorMessage = "Please enter the hourly rate.")]
		[Range(0.01, 1000, ErrorMessage = "Hourly rate must be between 0.01 and 1000.")]
		public decimal HourlyRate { get; set; }

		public List<IFormFile> SupportingDocuments { get; set; }
	}
}
