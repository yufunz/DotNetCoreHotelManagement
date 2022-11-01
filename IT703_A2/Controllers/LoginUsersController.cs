using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IT703_A2.Services;
using IT703_A2.Models.LoginUsers;

namespace IT703_A2.Controllers
{
    public class LoginUsersController : Controller
    {
        private readonly ILoginUsersService loginService;

        public LoginUsersController(ILoginUsersService lService)
        {
            this.loginService = lService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginUsersFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return this.View(user);
            }


            var isUserLoggedIn = await this.loginService.IsUserExist(user);

            if (isUserLoggedIn == null)
            {
                return this.InvalidCredentials(user);
            }

            var isPassExist = await this.loginService.IsPasswordCorrect(isUserLoggedIn, user);

            if (!isPassExist)
            {
                return this.InvalidCredentials(user);
            }

            await this.loginService.Login(isUserLoggedIn);


            return this.RedirectToAction("All", "Home");
        }

        private IActionResult InvalidCredentials(LoginUsersFormModel user)
        {
            const string errMsg = "Invalid Credentials!";

            ModelState.AddModelError(string.Empty, errMsg);

            return this.View(user);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await this.loginService.LogOut();

            return this.RedirectToAction("Index");
        }

    }
}
