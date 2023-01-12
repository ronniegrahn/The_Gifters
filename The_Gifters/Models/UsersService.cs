using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using The_Gifters.Views.Users;

namespace The_Gifters.Models
{
    public class UsersService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public UsersService(IdentityDbContext identityDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            identityDbContext.Database.EnsureCreated();
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<string> TryRegisterAsync(CreateVM viewModel)
        {
            // Todo: Try to create a new user
            IdentityUser identityUser = new IdentityUser
            {
                UserName = viewModel.FirstName,
                Email = viewModel.Email, 
                PhoneNumber = viewModel.PhoneNumber,
            };

            IdentityResult createResult = await
                userManager.CreateAsync(identityUser, viewModel.Password);

            bool createSucceeded = createResult.Succeeded;

            if (createSucceeded)
                return null;
            else
                return createResult.Errors.First().Description;
        }

        //public async Task<string> TryRegisterAsync(CreateVM viewModel)
        //{
        //    // Todo: Try to create a new user
        //    IdentityUser identityUser = new IdentityUser
        //    {
        //        UserName = viewModel.FirstName,
        //        Email = viewModel.LastName
        //    };

        //    IdentityResult createResult = await
        //        userManager.CreateAsync(identityUser, viewModel.Password);

        //    bool createSucceeded = createResult.Succeeded;

        //    if (createSucceeded)
        //        return null;
        //    else
        //        return createResult.Errors.First().Description;
        //}

        public async Task<bool> TryLoginAsync(LoginVM viewModel)
        {
            // Todo: Try to sign user
            SignInResult signInResult = await signInManager.PasswordSignInAsync(
                viewModel.Username,
                viewModel.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            bool signInSucceeded = signInResult.Succeeded;

            return signInSucceeded;
        }
    }
}
