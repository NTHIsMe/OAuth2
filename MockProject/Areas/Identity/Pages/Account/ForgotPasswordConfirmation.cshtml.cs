using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MockProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		public ForgotPasswordConfirmation(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}
		
		public void OnGet()
		{
        }
	}
}
