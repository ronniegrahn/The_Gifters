using System.ComponentModel.DataAnnotations;

namespace The_Gifters.Views.Users
{
	public class LoginVM
	{
		[Required]
		[EmailAddress]
		[Display(Name = "E-mail")]
		public string Username { get; set; }

        [Required(ErrorMessage = "Enter a password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
