using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ILogger<PlatformsController> _logger;
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public PlatformsController(ILogger<PlatformsController> logger, ICommandRepo repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platforms = _repo.GetAllPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpPost]
    public ActionResult TestInboundConnection(PlatformReadDto platform)
    {
        Console.WriteLine("--> Inbound POST @ Command Service");
        _logger.LogInformation(JsonSerializer.Serialize(platform));
        return Ok("test commands platforms connection");
    }
}