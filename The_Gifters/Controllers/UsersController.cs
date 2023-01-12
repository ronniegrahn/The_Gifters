using Microsoft.AspNetCore.Mvc;
using The_Gifters.Models;
using The_Gifters.Views.Participations;
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

            // Check if credentials is valid (and set auth cookie).
            var success = await usersService.TryLoginAsync(viewModel);

            if (!success)
            {
                // Show error message if login failed.
                ModelState.AddModelError(string.Empty, "Login failed");
                return View();
            }

            // Redirect user when succcessfully logged in.
            return RedirectToAction(nameof(Index));
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

            // Below tries to create new user and return a message
            // where 'null' is success and 'not null' means that
            // there is an error.
            var errorMessage = await usersService.TryRegisterAsync(viewModel);

            if (errorMessage != null)
            {
                // This here shows error message to user if there is one.
                ModelState.AddModelError(string.Empty, errorMessage);
                return View();
            }

            // When new user is created user is redirected to the
            // login page. Can be improved into automated loggin in.
            return RedirectToAction(nameof(Login));
        }
    }
}
