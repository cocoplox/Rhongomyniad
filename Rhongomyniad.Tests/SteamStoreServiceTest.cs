using Rhongomyniad.Application.Services;

namespace Rhongomyniad.Tests;

[TestFixture]
public class SteamStoreServiceTest
{
    [Test]
    public async Task CallsApiCorrectly()
    {
        //Arrange
        //Dyson Sphere Program, Persona 3 Reload
        int[] ids = new[] { 1366540, 2161700 };
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://store.steampowered.com/api/");
        var steamStoreService = new SteamStoreService(httpClient);
        
        //Act
        var result = await steamStoreService.GetAppDetails(ids);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Any(), Is.True);
        Assert.That(result.Keys.Count, Is.EqualTo(ids.Length));
        Assert.That(result.Keys, Is.EquivalentTo(ids));
    }
}