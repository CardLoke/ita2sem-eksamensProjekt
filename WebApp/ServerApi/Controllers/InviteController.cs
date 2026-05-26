using Core.Model;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Interfaces;

namespace ServerApi.Controllers;

[ApiController]
[Route("api/invite")]
public class InviteController : ControllerBase
{
    private readonly IInvite inviteRepo;

    public InviteController(IInvite _repo)
    {
        inviteRepo = _repo;
    }

    [HttpPost]
    public IActionResult Invite(Invite invite)
    {
        inviteRepo.PostInvite(invite);
        return Ok(new { message = "Invite created successfully" });
    }

    [HttpGet]
    [Route("get/{username}")]
    public IActionResult GetInvites(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return BadRequest("Username is required");

        var invites = inviteRepo.GetInvites(username);
        return Ok(invites);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        inviteRepo.Delete(id);
        return Ok(new { message = "Invite deleted successfully" });
    }
}
