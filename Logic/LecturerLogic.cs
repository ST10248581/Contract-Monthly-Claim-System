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
                                  select new ClaimResult
                                  {
                                      HourlyRate = c.HourlyRate,
                                      HoursWorked = c.HoursWorked,
                                      ExpectedPayout = c.HoursWorked * c.HourlyRate,
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

