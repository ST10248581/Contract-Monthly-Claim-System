using System.ComponentModel.DataAnnotations;


namespace CMCS.Models
{
	public class LecturerClaimRequest
	{
		[Required]
		public string LecturerId { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number of hours.")]
		public int HoursWorked { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime DateWorked { get; set; }

		[Required]
		public IEnumerable<IFormFile> SupportingDocuments { get; set; }
	}
}
