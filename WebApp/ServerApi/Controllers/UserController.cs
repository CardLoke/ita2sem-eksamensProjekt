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
    public void SignUp(User user)
    {
        userRepo.SignUp(user);
    }
}
