using Microsoft.AspNetCore.Mvc;
using The_Gifters.Models;
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
