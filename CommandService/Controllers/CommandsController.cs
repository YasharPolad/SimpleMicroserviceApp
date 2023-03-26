using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;

namespace CommandService.Controllers;

[ApiController]
[Route("api/c/platforms/{platformId}/[controller]")]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        if (!_repo.PlatformExists(platformId))
            return NotFound();
        
        var commands = _repo.GetCommandsForPlatform(platformId);
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{commandId}")]
    public ActionResult<CommandReadDto> GetCommand(int commandId)
    {
        var command = _repo.GetCommand(commandId);
        if (command is null)
            return NotFound();

        return Ok(_mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandDto, int platformId)
    {
        var command = _mapper.Map<Command>(commandDto);
        command.PlatformId = platformId;
        
        _repo.CreateCommand(command);
        return Ok(_mapper.Map<CommandReadDto>(command));
    }
}