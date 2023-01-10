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
	}
}
