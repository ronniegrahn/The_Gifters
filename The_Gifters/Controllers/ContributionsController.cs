using Microsoft.AspNetCore.Mvc;

namespace The_Gifters.Controllers
{
	public class ContributionsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
