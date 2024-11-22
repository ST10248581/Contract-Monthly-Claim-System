
using CMCS.Models;
using CMCS.Repository;

namespace CMCS.Logic
{
    public class ClaimProcessingLogic
    {

        //Approval Criteria
        int minimumPayoutHours = 16;
        decimal minimumHourlyRate = 50;
        decimal maximumHourlyRate = 200;
        int minimumSupportingDocumentsNeeded = 1;

        public void PreProcessClaim(int claimId, int userId)
        {
            using (var dm = new DataModel())
            {
                
                
                var claim = dm.Claims.FirstOrDefault(c => c.ClaimId == claimId);
                if (claim == null) throw new Exception("Claim not found.");

                if (claim.HourlyRate < minimumHourlyRate && claim.HourlyRate > maximumHourlyRate) claim.Status = "Preprocessed status: Rejected (Invalid hourly rate)";

                var supportingDocuments = dm.ClaimSupportingDocuments.Where(d => d.ClaimId == claimId).ToList();

                if (supportingDocuments == null || supportingDocuments.Count < minimumSupportingDocumentsNeeded) claim.Status = "Preprocessed status: Rejected (More Supporting documents needed)";

                if (claim.HoursWorked >= minimumPayoutHours)
                {
                    claim.Status = "Preprocessed status: Approved";
                }
                else
                {
                    claim.Status = "Preprocessed status: Rejected (Minimum hours not worked)";
                }

                claim.ReviewedDate = DateOnly.FromDateTime(DateTime.Now);
                claim.ApprovedByProgrammeManagerId = userId;

                dm.SaveChanges();
            }
            
        }

        public ClaimListResultModel GetAllClaims()
        {
            using (var dm = new DataModel())
            {
                return new ClaimListResultModel()
                {

                    LecturerClaims = (from c in dm.Claims
                                      select new ClaimResult
                                      {
                                          ClaimId = c.ClaimId,
                                          LecturerId = c.LecturerId,
                                          HourlyRate = c.HourlyRate,
                                          HoursWorked = c.HoursWorked,
                                          ExpectedPayout = c.HoursWorked * c.HourlyRate,
                                          Status = c.Status
                                      }).ToList()

                };
            }

        }
    }
}
