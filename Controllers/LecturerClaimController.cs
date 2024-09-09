using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
	public class LecturerClaimController : Controller
	{
        [HttpGet]
		public IActionResult Index()
		{
			return View("SubmitLecturerClaim");
		}

        [HttpPost]
        public IActionResult Index(LecturerClaimRequest request)
        {
            return View("SubmitClaimSuccess");
        }

        public IActionResult TrackClaims()
        {
            return View("TrackClaim");
        }
    } 
    
}
