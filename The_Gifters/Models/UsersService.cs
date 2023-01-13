using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using The_Gifters.Models.Entities;
using The_Gifters.Views.Users;

namespace The_Gifters.Models
{
    public class UsersService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        GiftersContext giftersContext;

        public UsersService(IdentityDbContext identityDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, GiftersContext giftersContext)
        {
            identityDbContext.Database.EnsureCreated();
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.giftersContext = giftersContext;
        }

        public async Task<string> TryRegisterAsync(CreateVM viewModel)
        {
            // Below attempts to create a new user.
            IdentityUser identityUser = new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email, 
                PhoneNumber = viewModel.PhoneNumber,
            };

            IdentityResult createResult = await
                userManager.CreateAsync(identityUser, viewModel.Password);

            bool createSucceeded = createResult.Succeeded;

            if (createSucceeded)
            {
                giftersContext.Customers.Add(new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    AspNetUsersId = identityUser.Id
                });
                giftersContext.SaveChanges();
                return null;

            }
            else
                return createResult.Errors.First().Description;
        }

        public async Task<bool> TryLoginAsync(LoginVM viewModel)
        {
            // Below tries to sign in user.
            SignInResult signInResult = await signInManager.PasswordSignInAsync(
                viewModel.Username,
                viewModel.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            bool signInSucceeded = signInResult.Succeeded;

            // Returns 'true' if sign in successful
            // and 'false' if sign in not successful.
            return signInSucceeded;
        }
    }
}
