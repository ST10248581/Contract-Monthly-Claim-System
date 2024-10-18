using CMCS.Data;

namespace CMCS.Models
{
    public class ClaimListResultModel
    {
        public List<ClaimResult> LecturerClaims { get; set; }
    }

    public class ClaimResult
    {
        public Guid ClaimId { get; set; }
		public Guid LecturerId { get; set; }
		public int HoursWorked { get; set; }

		public decimal HourlyRate { get; set; }

        public decimal ExpectedPayout { get; set; }
		public IEnumerable<IFormFile> SupportingDocuments { get; set; }
		public string Status { get; set; } = "Pending";
	}
}
