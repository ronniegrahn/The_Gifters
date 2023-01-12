using Microsoft.AspNetCore.Mvc;
using The_Gifters.Models;
using The_Gifters.Views.Contributions;
using The_Gifters.Views.Users;

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

        [HttpPost("users/login")]
        public async Task<IActionResult> LoginAsync(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            // Check if credentials is valid (and set auth cookie)
            var success = await usersService.TryLoginAsync(viewModel);
            if (!success)
            {
                // Show error
                ModelState.AddModelError(string.Empty, "Login failed");
                return View();
            }

            // Redirect user
            //return RedirectToAction(nameof(Members));
            return Content("You have logged in!");
            //return RedirectToAction(nameof(ParticipationsController.MyParticipations));
        }

        [HttpGet("users/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateAsync(CreateVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            // Try to register user
            var errorMessage = await usersService.TryRegisterAsync(viewModel);

            if (errorMessage != null)
            {
                // Show error
                ModelState.AddModelError(string.Empty, errorMessage);
                return View();
            }

            //// This is my own test of 'errorMessage'
            //if (errorMessage != null)
            //{
            //    return Content("Something went wrong!");
            //}

            return RedirectToAction(nameof(Login));
            //return View();
        }
    }
}
