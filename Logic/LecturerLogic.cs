using CMCS.Data;
using CMCS.Models;

namespace CMCS.Logic
{
    public class LecturerLogic
    {
        public ClaimListResultModel GetLecturerClaims()
        {
            return new ClaimListResultModel()
            {
                LecturerClaims = (from c in ClaimManager.Claims
                                  select new Claim
                                  {
                                      LecturerId = c.LecturerId,
                                      HourlyRate = c.HourlyRate,
                                      HoursWorked = c.HoursWorked,
                                      SupportingDocuments = c.SupportingDocuments,
                                      Status = c.Status
                                  }).ToList()
            };
        }

        public void SubmitLecturerClaim(Claim claim)
        {
            ClaimManager.Claims.Add(claim);
        }

    }
}

