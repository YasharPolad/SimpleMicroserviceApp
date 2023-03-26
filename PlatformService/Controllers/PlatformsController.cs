using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;

    public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient)
    {
        _repo = repo;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
    {
        var platforms = _repo.GetAll();
        var platformDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
        return Ok(platformDtos);
    }
    
    [HttpGet("{id}")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platform = _repo.GetById(id);
        if (platform is null)
            return NotFound();
        var platformDto = _mapper.Map<PlatformReadDto>(platform);
        return platformDto;
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformIn)
    {
        var platform = _mapper.Map<Platform>(platformIn);
        _repo.Create(platform);
        
        var platformOut = _mapper.Map<PlatformReadDto>(platform);

        try
        {
            await _commandDataClient.SendPlatformToCommand(platformOut);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        return platformOut;
    }
}