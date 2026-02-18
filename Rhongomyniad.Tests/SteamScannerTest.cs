using Moq;
using Rhongomyniad.Application.Services;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Infrastructure.Scanners;

namespace Rhongomyniad.Tests;
[TestFixture]
public class SteamScannerTest
{
    [Test]
    public async Task ScaneaBien()
    {
        //Setup
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://store.steampowered.com/api/");
        SteamStoreService steamStoreService = new(httpClient);
        var sut = new SteamGameScanner(steamStoreService);
        
        //Act
        await sut.ScanAsync();
    }
}