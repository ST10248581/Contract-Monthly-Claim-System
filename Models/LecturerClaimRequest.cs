using System.ComponentModel.DataAnnotations;


namespace CMCS.Models
{
	public class LecturerClaimRequest
	{

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number of hours.")]
		public int HoursWorked { get; set; }

		[Required]
		public decimal HourlyRate { get; set; }

		public List<IFormFile> SupportingDocuments { get; set; }
	}
}
