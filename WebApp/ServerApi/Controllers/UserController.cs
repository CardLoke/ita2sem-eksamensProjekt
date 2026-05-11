using Core.Model;
using ServerApi.Interfaces;
using ServerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace ServerApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUser userRepo;
    public UserController(IUser _repo)
    {
        userRepo = _repo;
    }
    [HttpPost]
    [Route("signUp")]
    public string SignUp(User user)
    {
        var result=userRepo.SignUpValidation(user.Username, user.Mail);
        if (result is null)
        {
            userRepo.SignUp(user);
            Console.WriteLine("succes");
            return "Sign Up was succesfull!";
        }
        else
        {
            Console.WriteLine("Fail");
            return "Username or Email already exists!";
        }

    }
    [HttpPost]
    [Route("logIn")]
    public async Task<User?> LogIn(LoginRequest loginRequest)
    {
        Console.WriteLine("hej");
        User? loggedIn = await userRepo.LogIn(loginRequest);
        return loggedIn;
    }
    
    [HttpPut]
    [Route("edit")]
    public async Task Edit(User user)
    {
        await userRepo.Edit(user);
    }
    
}
