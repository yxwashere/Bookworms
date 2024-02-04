using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Bookworms.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

		public void OnGet()
		{
            var sessionAuthToken = HttpContext.Session.GetString("AuthToken");
            var cookieAuthToken = Request.Cookies["AuthToken"];
            var userEmail = TempData["UserEmail"] as string;
            var userPassword = TempData["UserPassword"] as string;
            var userFName = TempData["UserFName"] as string;
            var userLName = TempData["UserLName"] as string;
            var userAddress = TempData["UserAddress"] as string;
            var userMobileNo = TempData["UserMobileNo"] as string;
                
            if (sessionAuthToken == null || cookieAuthToken == null || sessionAuthToken != cookieAuthToken)
            {
                // Redirect to login page if session token and cookie token do not match
                Response.Redirect("/Login");
            }
            else
            {
                ViewData["UserEmail"] = userEmail;
                ViewData["UserPassword"] = userPassword;
                ViewData["UserFName"] = userFName;
                ViewData["UserLName"] = userLName;
                ViewData["UserAddress"] = userAddress;
                ViewData["UserMobileNo"] = userMobileNo;
            }
        }

 
    }
}