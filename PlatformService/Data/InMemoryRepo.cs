using PlatformService.Models;

namespace PlatformService.Data;

public class InMemoryRepo : IPlatformRepo
{
    private readonly AppDbContext _context;

    public InMemoryRepo(AppDbContext context)
    {
        _context = context;
    }
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public IEnumerable<Platform> GetAll()
    {
        return _context.Platforms.ToList();
    }

    public Platform GetById(int id)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public void Create(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }
        _context.Platforms.Add(platform);
        _context.SaveChanges();
    }
}