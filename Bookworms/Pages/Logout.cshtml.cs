using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Bookworms.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogoutModel(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			this.signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostLogoutAsync()
		{
            // Clear session variables
            _httpContextAccessor.HttpContext.Session.Clear();

            // Delete the AuthToken cookie
            Response.Cookies.Delete("AuthToken");

            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            if (Request.Cookies[".AspNetCore.Session"] != null)
            {
                Response.Cookies.Append(".AspNetCore.Session", "", new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(-20)
                });
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies.Append("AuthToken", "", new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(-20)
                });
            }

            // Redirect to login page
            return RedirectToPage("/Login");
        }
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
