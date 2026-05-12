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
    public async Task<IActionResult> SignUp(User user)   
    {
       
        var existingUser = await userRepo.SignUpValidation(user.Username, user.Mail);

        if (existingUser == null)
        {
            userRepo.SignUp(user);           
            
            return Ok("Sign Up was successful!");
        }
        else
        {
            
            return Conflict("Username or Email already exists!");  
        }
    }

    [HttpPost]
    [Route("logIn")]
    public async Task<User?> LogIn(LoginRequest loginRequest)
    {
        return await userRepo.LogIn(loginRequest);
    }

    [HttpPut]
    [Route("edit")]
    public async Task Edit(User user)
    {
        await userRepo.Edit(user);
    }
}