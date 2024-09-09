using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ClaimApprovalController : Controller
    {
        public IActionResult Index()
        {
            return View("ProcessClaims");
        }
    }
}
