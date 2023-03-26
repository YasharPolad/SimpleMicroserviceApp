using System.ComponentModel.DataAnnotations;

namespace CommandService.Models;

public class Command
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string HowTo { get; set; }
    
    [Required]
    public string CommandLine { get; set; }
    
    [Required]
    public int PlatformId { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(HowTo)}: {HowTo}, {nameof(CommandLine)}: {CommandLine}, {nameof(PlatformId)}: {PlatformId}, {nameof(Platform)}: {Platform}";
    }

    public Platform Platform { get; set; }
}