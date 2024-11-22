namespace CMCS.Models
{
    public class InvoiceList
    {
        public List<InvoiceItem> Invoices { get; set; }
    }

    public class InvoiceItem
    {
        public string Title { get; set; }
        public int LecturerId {  get; set; } 
        public string LecturerEmail { get; set; }

        public string LecturerFullName { get; set; }

        public string ClaimStatus { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }

        public int HoursWorked { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal TotalInvoiceAmount { get; set; }


    }
}
