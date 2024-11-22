using CMCS.Logic;
using CMCS.Models;
using CMCS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;

namespace CMCS.Controllers
{
    public class HRController : Controller
    {
        private int userId = 0;
        private int userRoleId = 0;
        private static int lecturerId = 0;

        private AuthLogic _authLogic;
        private HRLogic _hrLogic;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HRController(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;

            _authLogic = new AuthLogic();
            _hrLogic = new HRLogic();

            userId = _authLogic.AuthenticateUser(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            userRoleId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userRoleId"));
            if (userRoleId == 0) throw new Exception("User role not found.");

        }

        [HttpGet]
        public IActionResult AutoGenerateInvoices()
        {
            try
            {
                if (userId == 0) throw new Exception("User not logged in.");

                bool hasAccess = _authLogic.authorizeVeiwClaim(userRoleId);
                if (!hasAccess) throw new Exception("Unauthorized access to HR Features");

                var invoices = _hrLogic.AutoGenerateInvoices();

                return View("AutoGenerateInvoices", invoices);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

		[HttpGet]
		public IActionResult GetLecturerDetails(int id)
		{
			try
			{
                using (var dm = new DataModel())
                {
                    if (userId == 0) throw new Exception("User not logged in.");

                    bool hasAccess = _authLogic.authorizeUpdateUser(userRoleId);
                    if (!hasAccess) throw new Exception("Unauthorized access to HR Features");

                    lecturerId = id;

                    var user = dm.SystemUsers.FirstOrDefault(u => u.UserId == lecturerId);
                    if (user == null) throw new Exception("Lecturer not found.");

                    return View("UpdateLecturerDetails", new LecturerDetailsRequest()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    });
                }
				
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction("Error", "Home");
			}
		}

        [HttpGet]
        public IActionResult GetAllLecturers()
        {
            try
            {
                using (var dm = new DataModel())
                {
                    if (userId == 0) throw new Exception("User not logged in.");

                    bool hasAccess = _authLogic.authorizeUpdateUser(userRoleId);
                    if (!hasAccess) throw new Exception("Unauthorized access to HR Features");

                    var users =( from l in dm.SystemUsers
                                where l.UserRole == 1
                                select new LecturerItem()
                                {
                                    UserId = l.UserId,
                                    FirstName = l.FirstName,
                                    LastName = l.LastName,
                                    Email = l.Email,
                                    UserRole = l.UserRole
                                }).ToList();

                return View("GetAllLecturers",new LecturerListResult()
                {
                    Lecturers = users
                });
                }
				
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction("Error", "Home");
			}
		}

        [HttpPost]
        public IActionResult UpdateLecturerDetails(LecturerDetailsRequest request)
        {
            try
            {
                using (var dm = new DataModel())
                {
                    if (userId == 0) throw new Exception("User not logged in.");

                    bool hasAccess = _authLogic.authorizeUpdateUser(userRoleId);
                    if (!hasAccess) throw new Exception("Unauthorized access to HR Features");

                    var user = dm.SystemUsers.FirstOrDefault(u => u.UserId == lecturerId);
                    if (user == null) throw new Exception("Lecturer not found.");


					if (!ModelState.IsValid)
					{
						return View("GetLecturerDetails", request);
					}
					user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Email = request.Email;

                    dm.SaveChanges();

					return RedirectToAction("GetAllLecturers");
				}
			}          
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction("Error", "Home");
			}
		}
    }
}
