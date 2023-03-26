using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos;

public class PlatformCreateDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Publisher { get; set; }
    
    [Required]
    public string Cost { get; set; }

    public override string ToString()
    {
        return $"{{{nameof(Name)}: {Name}, {nameof(Publisher)}: {Publisher}, {nameof(Cost)}: {Cost}";
    }
}