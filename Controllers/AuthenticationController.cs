using CMCS.Logic;
using CMCS.Models;
using CMCS.Repository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class AuthenticationController : Controller
    {
        private AuthLogic _authLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IHttpContextAccessor httpContextAccessor)
        {
            _authLogic = new AuthLogic();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {            
                return View();           
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel request)
        {
            try
            {
                using (var dm = new DataModel())
                {
                    var passwordHash = _authLogic.HashPassword(request.Password);

                    var user = dm.SystemUsers.FirstOrDefault(u => u.Email == request.Email && u.PasswordHash == passwordHash);

                    if (user == null) throw new Exception("Incorrect Email or Password. Please try again");

                    _httpContextAccessor.HttpContext.Session.SetString("userId", user.UserId.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("userRoleId", user.UserRole.ToString());
                }

                return View("LoginSuccess");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            try
            {
                using (var dm = new DataModel())
                {
                    TempData["userId"] = null;
                    TempData["userRoleId"] = null;
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
