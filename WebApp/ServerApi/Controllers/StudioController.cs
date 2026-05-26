using Core.Model;
using ServerApi.Interfaces;
using ServerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace ServerApi.Controllers;

[ApiController]
[Route("api/studio")]
public class StudioController : ControllerBase
{
    private readonly IStudio studioRepo;

    public StudioController(IStudio _repo)
    {
        studioRepo = _repo;
    }

    [HttpPost]
    [Route("register")]
    public void RegisterStudio(Studio studio)
    {
        studioRepo.RegisterStudio(studio);
    }

    [HttpGet]
    
    public List<Studio> Get()
    {
        return studioRepo.GetAll();
    }
    [HttpDelete]
    [Route("delete/{id:int}")]
    public void Delete(int id)
    {
        studioRepo.Delete(id);
    }

    [HttpPut]
    [Route("edit")]
    public async Task Edit(Studio studio)
    {
        await studioRepo.Edit(studio);
    }
    [HttpPut]
    [Route("invite")]
    public void Invite(Invite invite)
    {
        studioRepo.Invite(invite);
    }
}