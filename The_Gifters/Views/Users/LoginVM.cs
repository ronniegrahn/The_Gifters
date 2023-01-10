using System.ComponentModel.DataAnnotations;

namespace The_Gifters.Views.Users
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Enter an email")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Enter a password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
