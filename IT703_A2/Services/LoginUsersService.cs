using IT703_A2.Models.LoginUsers;
using IT703_A2.Models;
using Microsoft.AspNetCore.Identity;

namespace IT703_A2.Services
{
    public class LoginUsersService : ILoginUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginUsersService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task Login(User user)
        {
            await this.signInManager.SignInAsync(user, true);
        }

        public async Task<User> IsUserExist(LoginUsersFormModel user)
        {
            return await this.userManager.FindByEmailAsync(user.Email);
        }

        public async Task<bool> IsPasswordCorrect(User user, LoginUsersFormModel userFormModel)
        {
            return await this.userManager.CheckPasswordAsync(user, userFormModel.Password);
        }

        public async Task LogOut()
        {
            await this.signInManager.SignOutAsync();
        }

    }
}
