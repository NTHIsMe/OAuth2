using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MockProjectta;

namespace MockProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly ApplicationDbContext _db;


		public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewData["TwoFactorEnabled"] = false;
            }
            else
            {
                ViewData["TwoFactorEnabled"] = user.TwoFactorEnabled;
            }
            return View();
        }
		public IActionResult IndexUser()
		{
			var roles = _db.Users.ToList();
			return View(roles);
		}
		[HttpGet]
		public IActionResult Upsert(string id)
		{
			if (String.IsNullOrEmpty(id))
			{
				return View();
			}
			else
			{
				var objFromDb = _db.Users.FirstOrDefault(u => u.Id == id);
				return View(objFromDb);
			}


		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upsert(IdentityUser roleObj)
		{
			var checkExist = await _userManager.FindByEmailAsync(roleObj.Email);
			if (checkExist != null)
			{
				TempData[SD.Error] = "User already exists.";
				return RedirectToAction(nameof(Index));
			}
			if (string.IsNullOrEmpty(roleObj.Id))
			{
				await _userManager.CreateAsync(new IdentityUser() { Id = Guid.NewGuid().ToString(), UserName = roleObj.UserName, Email = roleObj.Email }) ;
				TempData[SD.Success] = "User created successfully";
			}
			else
			{
				var objRoleFromDb = _db.Users.FirstOrDefault(u => u.Id == roleObj.Id);
				if (objRoleFromDb == null)
				{
					TempData[SD.Error] = "User not found.";
					return RedirectToAction(nameof(Index));
				}
				objRoleFromDb.UserName = roleObj.UserName;
				objRoleFromDb.Email = roleObj.Email.ToUpper();
				var result = await _userManager.UpdateAsync(objRoleFromDb);
				TempData[SD.Success] = "User updated successfully";
			}
			return RedirectToAction(nameof(Index));

		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string id)
		{
			var objFromDb = _db.Users.FirstOrDefault(u => u.Id == id);
			if (objFromDb == null)
			{
				TempData[SD.Error] = "User not found.";
				return RedirectToAction(nameof(Index));
			}
			var userRolesForThisRole = _db.UserRoles.Where(u => u.RoleId == id).Count();
			if (userRolesForThisRole > 0)
			{
				TempData[SD.Error] = "Cannot delete this role, since there are users assigned to this role.";
				return RedirectToAction(nameof(Index));
			}
			await _userManager.DeleteAsync(objFromDb);
			TempData[SD.Success] = "User deleted successfully.";
			return RedirectToAction(nameof(Index));

		}

		[Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AccessDenied() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
