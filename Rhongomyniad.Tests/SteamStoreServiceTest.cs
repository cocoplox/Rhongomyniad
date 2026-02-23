using Microsoft.Extensions.Logging;
using Moq;
using Rhongomyniad.Application.Services;

namespace Rhongomyniad.Tests;

[TestFixture]
public class SteamStoreServiceTest
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
    public async Task CallsApiCorrectly()
    {
        //Arrange
        //Dyson Sphere Program, Persona 3 Reload
        int[] ids = new[] { 1366540, 2161700 };
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://store.steampowered.com/api/");
        var steamStoreService = new SteamStoreService(httpClient,  _logger);
        
        //Act
        var result = await steamStoreService.GetAppDetails(ids);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Any(), Is.True);
        Assert.That(result.Keys.Count, Is.EqualTo(ids.Length));
        Assert.That(result.Keys, Is.EquivalentTo(ids));
    }
}