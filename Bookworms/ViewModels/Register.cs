using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Bookworms.Attributes;

namespace Bookworms.ViewModels
{
	public class Register
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[Display(Name = "Credit Card No")]
		public string CreditCardNo { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Mobile No")]
		public string MobileNo { get; set; }

		[Required]
		[Display(Name = "Billing Address")]
		public string BillingAddress { get; set; }

		[Required]
		[Display(Name = "Shipping Address")]
		public string ShippingAddress { get; set; }

    }
}

