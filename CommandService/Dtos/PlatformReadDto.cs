﻿namespace PlatformService.Dtos;

public class PlatformReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
    }
}