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
    public void registerStudio(Studio studio)
    {
        studioRepo.registerStudio(studio);
    }

    [HttpGet]
    [Route("loadStudio")]
    public List<Studio> Get()
    {
        return studioRepo.GetAll();
    }
    
}