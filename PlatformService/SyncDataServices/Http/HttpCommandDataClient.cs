using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json"
        );

        // var httpC = JsonContent.Create<PlatformReadDto>(platform, MediaTypeHeaderValue.Parse("application/json"));
        _httpClient.BaseAddress = new Uri(_configuration["CommandService"]);
        var response = await _httpClient.PostAsync($"/api/c/platforms", httpContent);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to Command Service was OK!");
        }
        else
        {
            Console.WriteLine("--> Failed POST to command service");
        }
    }
}