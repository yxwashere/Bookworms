using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Encodings.Web;

namespace Bookworms.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly HtmlEncoder _htmlEncoder;

        public LoginModel(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, HtmlEncoder htmlEncoder)
        {
			this.signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            _htmlEncoder = htmlEncoder;
        }

		public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var sanitizedEmail = _htmlEncoder.Encode(LModel.Email);
                var sanitizedPassword = _htmlEncoder.Encode(LModel.Password);

                var result = await signInManager.PasswordSignInAsync(sanitizedEmail, sanitizedPassword , LModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    // Generate a unique token (GUID)
                    var authToken = Guid.NewGuid().ToString();

                    // Save the token in session
                    HttpContext.Session.SetString("AuthToken", authToken);

                    // Save the token in cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddMonths(1)
                    };
                    Response.Cookies.Append("AuthToken", authToken, cookieOptions);

                    var user = await userManager.FindByEmailAsync(LModel.Email);

                    TempData["UserEmail"] = user.Email; // Store email in TempData
                    TempData["UserPassword"] = sanitizedPassword;
                    TempData["UserAddress"] = user.BillingAddress;
                    TempData["UserFName"] = user.FirstName;
                    TempData["UserLName"] = user.LastName;
                    TempData["UserMobileNo"] = user.MobileNo;

                    return RedirectToPage("Index");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Account locked due to too many failed login attempts. Please try again later.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "You are not allowed to log in. Please contact the administrator.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }

            // If we reach here, something went wrong with the login attempt
            return Page();
        }
    }
}
