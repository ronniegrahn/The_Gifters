using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace The_Gifters.Controllers
{
	public class ParticipationsController : Controller
	{
		[Authorize]
		[HttpGet("participations/participate")]
		public IActionResult Participate()
		{
			return View();
		}

		[Authorize]
		[HttpGet("participations/myparticipations")]
		public IActionResult MyParticipations()
		{
			return View();
		}

        [Authorize]
        [HttpGet("participations/details")]
		public IActionResult Details()
		{
			return View();
		}
	}
}
