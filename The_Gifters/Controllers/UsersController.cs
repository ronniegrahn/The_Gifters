using Microsoft.AspNetCore.Mvc;
using The_Gifters.Models;

namespace The_Gifters.Controllers
{
	public class UsersController : Controller
	{
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
		{
            this.usersService = usersService;
        }

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
