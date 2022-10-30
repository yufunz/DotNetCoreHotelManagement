using IT703_A2.Models.LoginUsers;
using IT703_A2.Models;

namespace IT703_A2.Services
{
    public interface ILoginUsersService
    {
        Task Login(User user);

        Task<User> IsUserExist(LoginUsersFormModel user);

        Task<bool> IsPasswordCorrect(User user, LoginUsersFormModel userFormModel);

        Task LogOut();
    }
}
