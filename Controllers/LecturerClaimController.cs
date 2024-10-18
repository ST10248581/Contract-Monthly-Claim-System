using CMCS.Data;
using CMCS.Logic;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
	public class LecturerClaimController : Controller
	{
        private LecturerLogic _lecturerLogic;

        public LecturerClaimController()
        {
            _lecturerLogic = new LecturerLogic();
        }

        [HttpGet]
		public IActionResult Index()
		{
			return View("SubmitLecturerClaim");
		}

        [HttpPost]
        public IActionResult Index(LecturerClaimRequest request)
        {
            try
            {
                var files = Request.Form.Files;

                Claim claim = new Claim()
                {
                    ClaimId = Guid.NewGuid(),
                    LecturerId = Guid.NewGuid(),
                    HourlyRate = request.HourlyRate,
                    HoursWorked = request.HoursWorked,
                    SupportingDocuments = new List<IFormFile>()
                };

                if(request.SupportingDocuments != null)
                {
                    foreach (var file in request.SupportingDocuments)
                    {
                        claim.SupportingDocuments.Add(file);
                    }
                }
               

                _lecturerLogic.SubmitLecturerClaim(claim);
                return View("SubmitClaimSuccess");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult TrackClaims()
        {
            try
            {                
                var claims = _lecturerLogic.GetLecturerClaims();

                return View("TrackClaim", claims);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
           
        }
      
    } 
    
}
