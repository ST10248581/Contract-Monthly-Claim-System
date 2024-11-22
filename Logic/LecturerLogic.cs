
using CMCS.Models;
using CMCS.Repository;

namespace CMCS.Logic
{
    public class LecturerLogic
    {
        public ClaimListResultModel GetLecturerClaims(int lecturerId)
        {
            using (var dm = new DataModel())
            {
                return new ClaimListResultModel()
                {
                    
                    LecturerClaims = (from c in dm.Claims
                                      where c.LecturerId == lecturerId
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

        public void SubmitLecturerClaim(Claim claim)
        {
            using (var dm = new DataModel())
            {
                dm.Claims.Add(claim);
                dm.SaveChanges();
            }
        }

        public void AddDocumentToClaim(byte[] document, int claimId)
        {
            using (var dm = new DataModel())
            {
                dm.ClaimSupportingDocuments.Add(new ClaimSupportingDocument()
                {
                    ClaimId = claimId,
                    SupportingDocument = document
                });

                dm.SaveChanges();
            }
        }

    }
}

