using Microsoft.AspNetCore.Mvc;

namespace The_Gifters.Controllers
{
	public class ParticipationsController : Controller
	{
		[HttpGet("participations/participate")]
		public IActionResult Participate()
		{
			return View();
		}

		[HttpGet("participations/myparticipations")]
		public IActionResult MyParticipations()
		{
			return View();
		}

		[HttpGet("participations/details")]
		public IActionResult Details()
		{
			return View();
		}
	}
}
