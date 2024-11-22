using System.IO.Compression;
using System.Text;
using CMCS.Logic;
using CMCS.Models;
using CMCS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ClaimApprovalController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClaimProcessingLogic _claimProcessingLogic;
        private AuthLogic _authLogic;

        private int userId = 0;
        private int userRoleId = 0;

        public ClaimApprovalController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _claimProcessingLogic = new ClaimProcessingLogic();

            _authLogic = new AuthLogic();

            userId = _authLogic.AuthenticateUser(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            userRoleId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userRoleId"));
            if (userRoleId == 0) throw new Exception("User role not found.");

        }

        public IActionResult Index()
        {
            try
            {
                if (userId == 0) throw new Exception("User not logged in.");

                bool hasAccess = _authLogic.authorizeVeiwClaim(userRoleId);
                if (!hasAccess) throw new Exception("Unauthorized to view Claims");

                var claims = _claimProcessingLogic.GetAllClaims();

                return View("ProcessClaims", claims);
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
                using (var dm = new DataModel())
                {
                    if (userId == 0) throw new Exception("User not logged in.");

                    bool hasAccess = _authLogic.authorizeProcessClaim(userRoleId);
                    if (!hasAccess) throw new Exception("Unauthorized to processClaims");

                    var claim = dm.Claims.FirstOrDefault(c => c.ClaimId.ToString() == claimId);
                    if (claim == null) throw new Exception();

                    claim.ReviewedDate = DateOnly.FromDateTime(DateTime.Now);
                    claim.ApprovedByProgrammeManagerId = userId;

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

                    dm.SaveChanges();
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
