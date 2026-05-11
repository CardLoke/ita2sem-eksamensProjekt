using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{
    public interface IUser
    {
        void SignUp(User user);
        Task<User?> LogIn(LoginRequest loginRequest);
        Task Edit(User user);
        Task<User?> SignUpValidation(string username, string mail);
    }
}
