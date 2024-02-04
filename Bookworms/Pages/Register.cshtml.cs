using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms.ViewModels;
using Microsoft.AspNetCore.Identity;
using Bookworms.Model;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Bookworms.Pages
{
	public class RegisterModel : PageModel
	{

		private UserManager<ApplicationUser> userManager { get; }
		private SignInManager<ApplicationUser> signInManager { get; }
        private readonly HtmlEncoder _htmlEncoder;
        public string PasswordStatus { get; set; }

        [BindProperty]
		public Register RModel { get; set; }

		public RegisterModel(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager, HtmlEncoder htmlEncoder)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
            _htmlEncoder = htmlEncoder;
        }



		public void OnGet()
		{
		}


		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                var sanitizedEmail = _htmlEncoder.Encode(RModel.Email);
				var sanitizedFName = _htmlEncoder.Encode(RModel.FirstName);
				var sanitizedLName = _htmlEncoder.Encode(RModel.LastName);
				var sanitizedCCard = _htmlEncoder.Encode(RModel.CreditCardNo);
				var sanitizedPhone = _htmlEncoder.Encode(RModel.MobileNo);
				var sanitizedBAddress = _htmlEncoder.Encode(RModel.BillingAddress);
				var sanitizedSAddress = _htmlEncoder.Encode(RModel.ShippingAddress);
                var sanitizedPassword = _htmlEncoder.Encode(RModel.Password);

                var user = new ApplicationUser()
				{
					UserName = sanitizedEmail,
					Email = sanitizedEmail,
					FirstName = sanitizedFName,
					LastName = sanitizedLName,
					CreditCardNo = sanitizedCCard,
					MobileNo = sanitizedPhone,
					BillingAddress = sanitizedBAddress,
					ShippingAddress = sanitizedSAddress,
				};
                var result = await userManager.CreateAsync(user, RModel.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToPage("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
            }
			return Page();
		}

        private int checkPassword(string password)
        {
            int score = 0;

            // Score 1 (Complexity: Very Weak) – Min Password length of 8
            if (password.Length >= 8)
                score++;

            // Score 2 (Complexity: Weak) – Contains lowercase letter(s)
            if (Regex.IsMatch(password, "[a-z]"))
                score++;

            // Score 3 (Complexity: Medium) – Contains uppercase letter(s)
            if (Regex.IsMatch(password, "[A-Z]"))
                score++;

            // Score 4 (Complexity: Strong) – Contains numeral(s)
            if (Regex.IsMatch(password, @"\d"))
                score++;

            // Score 5 (Complexity: Excellent) – Contains special character(s)
            if (Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                score++;

            return score;
        }

        private string GetPasswordStrengthMessage(int score)
        {
            string strengthMessage;
            switch (score)
            {
                case 1:
                    strengthMessage = "Very Weak";
                    break;
                case 2:
                    strengthMessage = "Weak";
                    break;
                case 3:
                    strengthMessage = "Medium";
                    break;
                case 4:
                    strengthMessage = "Strong";
                    break;
                case 5:
                    strengthMessage = "Excellent";
                    break;
                default:
                    strengthMessage = "Unknown";
                    break;
            }
            return "Password Strength: " + strengthMessage;
        }
    }
}
