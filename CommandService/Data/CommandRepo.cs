using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public void CreatePlatform(Platform plat)
    {
        if (plat is null)
            throw new ArgumentNullException();
        _context.Platforms.Add(plat);

        _context.SaveChanges();
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);
    }

    public Command? GetCommand(int commandId)
    {
        return _context.Commands.FirstOrDefault(c => c.Id == commandId);
    }

    public void CreateCommand(Command command)
    {
        if (command is null)
        {
            throw new ArgumentNullException();
        }

        _context.Commands.Add(command);
        _context.SaveChanges();
    }
    
}