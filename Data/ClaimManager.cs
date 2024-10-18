using System.ComponentModel.DataAnnotations;

namespace CMCS.Data
{
    public class ClaimManager
    {
        public static List<Claim> Claims = new List<Claim>();
    }

    public class Claim
    {
        public Guid ClaimId { get; set; }
        public Guid LecturerId { get; set; }

        public int HoursWorked { get; set; }

        public decimal HourlyRate { get; set; }

        public List<IFormFile> SupportingDocuments { get; set; }
        public string Status { get; set; } = "Pending";
    }

}
