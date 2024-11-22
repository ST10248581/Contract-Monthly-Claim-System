using System.Reflection;
using CMCS.Logic;
using CMCS.Models;
using CMCS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
	public class LecturerClaimController : Controller
	{
        private LecturerLogic _lecturerLogic;
        private ClaimProcessingLogic _claimProcessingLogic;
        private AuthLogic _authLogic;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private int userId = 0;
        private int userRoleId = 0;

        public LecturerClaimController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _lecturerLogic = new LecturerLogic();
            _claimProcessingLogic = new ClaimProcessingLogic();
            _authLogic = new AuthLogic();


            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("userId");
            
            userId = _authLogic.AuthenticateUser(userIdString);

            userRoleId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userRoleId"));
            if (userRoleId == 0) throw new Exception("User role not found.");

        }

        [HttpGet]
		public IActionResult Index()
		{
            try
            {
                if (userId == 0) throw new Exception("User not logged in.");


                bool hasAccess = _authLogic.authorizeSubmitClaim(userRoleId);

                if (!hasAccess) throw new Exception("Unauthorized to submit Lecturer Claims. Please Login with or create a lecturer account.");


                return View("SubmitLecturerClaim");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
		}

        [HttpPost]
        public IActionResult Index(LecturerClaimRequest request)
        {
            try
            {
                           
                using (var dm = new DataModel())
                {
                    if (userId == 0) throw new Exception("User not logged in.");

                    bool hasAccess = _authLogic.authorizeSubmitClaim(userRoleId);

                    if (!hasAccess) throw new Exception("Unauthorized to submit Lecturer Claims. Please Login with or create a lecturer account.");


                    if (!ModelState.IsValid)
                    {
                        return View("SubmitLecturerClaim", request);
                    }

                    var files = Request.Form.Files;
 

                    Claim claim = new Claim()
                    {
                        LecturerId = userId,
                        ClaimDate = DateOnly.FromDateTime(DateTime.Now),
                        HourlyRate = request.HourlyRate,
                        HoursWorked = request.HoursWorked,
                        Status = "Pending",
                        ApprovedByProgrammeManagerId = null,
                        ReviewedDate = null
                    };

                    _lecturerLogic.SubmitLecturerClaim(claim);

                    if (request.SupportingDocuments != null)
                    {
                        foreach (var file in request.SupportingDocuments)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                file.CopyTo(memoryStream);
                                _lecturerLogic.AddDocumentToClaim(memoryStream.ToArray(), claim.ClaimId);
                            }
                        }
                    }



                    _claimProcessingLogic.PreProcessClaim(claim.ClaimId, userId);

                    return View("SubmitClaimSuccess");
                }
            
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult TrackClaims()
        {
            try
            {
                if (userId == 0) throw new Exception("User not logged in.");

                bool hasAccess = _authLogic.authorizeVeiwClaim(userRoleId);
                if (!hasAccess) throw new Exception("Unauthorized to track Lecturer Claims. Please Login with or create a lecturer account.");


                var claims = _lecturerLogic.GetLecturerClaims(userId);

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
