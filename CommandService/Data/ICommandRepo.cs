using CommandService.Models;

namespace CommandService.Data;

public interface ICommandRepo
{
    bool SaveChanges();

    IEnumerable<Platform> GetAllPlatforms();

    void CreatePlatform(Platform plat);

    bool PlatformExists(int platformId);


    IEnumerable<Command> GetCommandsForPlatform(int platformId);

    Command? GetCommand(int commandId);

    void CreateCommand(Command command);
}