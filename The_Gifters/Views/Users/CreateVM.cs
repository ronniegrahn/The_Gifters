using System.ComponentModel.DataAnnotations;

namespace The_Gifters.Views.Users
{
	public class CreateVM
	{
		[Required(ErrorMessage = "Enter a first name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Enter a last name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Enter an email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Enter a PhoneNumber")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Enter a password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Repeat password")]
		[Compare(nameof(Password))]
		public string PasswordRepeat { get; set; }

		[Required(ErrorMessage = "Accept terms and agreements")]
		public bool AcceptTerms { get; set; }
	}
}
