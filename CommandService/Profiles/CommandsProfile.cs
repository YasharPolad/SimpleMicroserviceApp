using AutoMapper;
using CommandService.Models;
using PlatformService.Dtos;

namespace CommandService.Profiles;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<Platform, PlatformReadDto>().ReverseMap();
        CreateMap<CommandReadDto, Command>().ReverseMap();
        CreateMap<Command, CommandReadDto>().ReverseMap();
    }
}