using Moq;
using Rhongomyniad.Application.Services;
using Rhongomyniad.Infrastructure.Scanners;
using Microsoft.Extensions.Logging;

namespace Rhongomyniad.Tests;
[TestFixture]
public class SteamScannerTest
{
    private ILogger<SteamStoreService> _logger;
    [SetUp]
    public void Setup()
    {
        var loggerMock = new Mock<ILogger<SteamStoreService>>();
        loggerMock.Setup(l => l.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()));
        _logger = loggerMock.Object;
    }
    [Test]
    public async Task ScaneaBien()
    {
        //Setup
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://store.steampowered.com/api/");
        
        
        SteamStoreService steamStoreService = new(httpClient, _logger);
        //var sut = new SteamGameScanner(steamStoreService);
        
        //Act
        //await sut.ScanAsync();
    }
}