using System.IO.Compression;
using System.Text;
using CMCS.Data;
using CMCS.Logic;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ClaimApprovalController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var claims = (from c in ClaimManager.Claims
                              select new ClaimResult()
                              {
                                  ClaimId = c.ClaimId,
                                  LecturerId = c.LecturerId,
                                  HourlyRate = c.HourlyRate,
                                  HoursWorked = c.HoursWorked,
                                  ExpectedPayout = c.HourlyRate * c.HoursWorked,
                                  SupportingDocuments = c.SupportingDocuments
                              }).ToList();


                return View("ProcessClaims", new ClaimListResultModel() { LecturerClaims = claims });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public JsonResult ProcessClaim(string claimId, string action)
        {
            bool isSuccess = false;

            try
            {
                var claim = ClaimManager.Claims.FirstOrDefault(c => c.ClaimId.ToString() == claimId);
                if (claim == null) throw new Exception();

                if (action == "approve")
                {
                    claim.Status = "Approved";
                    isSuccess = true;
                }
                else if (action == "reject")
                {
                    claim.Status = "Rejected";
                    isSuccess = true;
                }
            }
            catch
            {
                isSuccess = false;
            }

            return Json(new { success = isSuccess });
        }

    }
}
