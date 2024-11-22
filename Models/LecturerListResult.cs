namespace CMCS.Models
{
    public class LecturerListResult
    {
        public List<LecturerItem> Lecturers { get; set; }
    }

    public class LecturerItem
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int UserRole { get; set; }
    }
}
