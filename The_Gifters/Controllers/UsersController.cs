using Microsoft.AspNetCore.Mvc;

namespace The_Gifters.Controllers
{
	public class UsersController : Controller
	{
		[HttpGet("")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("users/login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpGet("users/create")]
		public IActionResult Create()
		{
			return View();
		}
	}
}
