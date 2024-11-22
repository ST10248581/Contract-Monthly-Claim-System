using System.Text;
using CMCS.Models;
using CMCS.Repository;

namespace CMCS.Logic
{
    public class HRLogic
    {

        public InvoiceList AutoGenerateInvoices()
        {
            var result = new InvoiceList()
            {
                Invoices = new List<InvoiceItem>()
            };

            using (var dm = new DataModel())
            {
                var allApprovedClaims = (from c in dm.Claims
                                        where c.ReviewedDate != null && c.ApprovedByProgrammeManagerId != 0
                                        select c).ToList();

                foreach (var claim in allApprovedClaims)
                {
                    var approver = dm.SystemUsers.FirstOrDefault(u => u.UserId == claim.ApprovedByProgrammeManagerId);
                    if (approver == null) throw new Exception("Approver not found");

                    var lecturer =  dm.SystemUsers.FirstOrDefault(u => u.UserId == claim.LecturerId);
                    if (lecturer == null) throw new Exception("Lecturer not found.");

                    result.Invoices.Add(new InvoiceItem()
                    {
                        Title = $"Invoice {claim.ClaimId}",
                        LecturerId = claim.LecturerId,
                        LecturerEmail = lecturer.Email,
                        LecturerFullName = $"{lecturer.FirstName} {lecturer.LastName}",
                        ClaimStatus = claim.Status,
                        ApproverName = $"{approver.FirstName} {approver.LastName}",
                        ApproverEmail = approver.Email,
                        HoursWorked = claim.HoursWorked,
                        HourlyRate = claim.HourlyRate,
                        TotalInvoiceAmount = claim.HourlyRate * claim.HoursWorked
                    });
                }

                return result;
            }
        }
    }
}
