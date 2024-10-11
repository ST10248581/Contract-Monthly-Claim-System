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
                                  LecturerId = c.LecturerId,
                                  HourlyRate = c.HourlyRate,
                                  HoursWorked = c.HoursWorked,
                                  ExpectedPayout = c.HourlyRate * c.HoursWorked,
                                  SupportingDocuments = c.SupportingDocuments
                              }).ToList();


                return View("ProcessClaims", new ClaimListResultModel() { LecturerClaims = claims }) ;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
