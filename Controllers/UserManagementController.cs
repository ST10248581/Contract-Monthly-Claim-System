using Azure.Core;
using CMCS.Logic;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class UserManagementController : Controller
    {
        private UserLogic _userLogic;


        private int userId = 0;
        bool hasAccess = false;

        public UserManagementController()
        {
            _userLogic = new UserLogic();

        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            try
            {
                return View("CreateUser");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
           
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            try
            {

				if (!ModelState.IsValid)
				{
					return View("CreateUser", request);
				}
				_userLogic.CreateUser(request);

               return View("RegisterSuccess");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
