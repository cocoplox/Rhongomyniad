using Rhongomyniad.Infrastructure.Services;

namespace Rhongomyniad.Tests;

[TestFixture]
public class SteamFileParserTest
{
    private string _testFilePath;
    private const string FILE_NAME = "appmanifest_367520.acf";
    
    [SetUp]
    public void Setup()
    {
        //Eliminamos si ya existe
        if (File.Exists(Path.Combine(Path.GetTempPath(), FILE_NAME)))
            File.Delete(Path.Combine(Path.GetTempPath(), FILE_NAME));
        
        var fileContent =
            "\"AppState\"\n{\n\t\"appid\"\t\t\"367520\"\n\t\"universe\"\t\t\"1\"\n\t\"LauncherPath\"\t\t\"C:\\\\Program Files (x86)\\\\Steam\\\\steam.exe\"\n\t\"name\"\t\t\"Hollow Knight\"\n\t\"StateFlags\"\t\t\"4\"\n\t\"installdir\"\t\t\"Hollow Knight\"\n\t\"LastUpdated\"\t\t\"1771313434\"\n\t\"LastPlayed\"\t\t\"0\"\n\t\"SizeOnDisk\"\t\t\"5197398400\"\n\t\"StagingSize\"\t\t\"0\"\n\t\"buildid\"\t\t\"21656419\"\n\t\"LastOwner\"\t\t\"76561198412733249\"\n\t\"DownloadType\"\t\t\"1\"\n\t\"UpdateResult\"\t\t\"0\"\n\t\"BytesToDownload\"\t\t\"1078097600\"\n\t\"BytesDownloaded\"\t\t\"1078097600\"\n\t\"BytesToStage\"\t\t\"5197398400\"\n\t\"BytesStaged\"\t\t\"5197398400\"\n\t\"TargetBuildID\"\t\t\"21656419\"\n\t\"AutoUpdateBehavior\"\t\t\"0\"\n\t\"AllowOtherDownloadsWhileRunning\"\t\t\"0\"\n\t\"ScheduledAutoUpdate\"\t\t\"0\"\n\t\"InstalledDepots\"\n\t{\n\t\t\"367521\"\n\t\t{\n\t\t\t\"manifest\"\t\t\"4709992615819941531\"\n\t\t\t\"size\"\t\t\"5197398400\"\n\t\t}\n\t}\n\t\"UserConfig\"\n\t{\n\t\t\"language\"\t\t\"english\"\n\t}\n\t\"MountedConfig\"\n\t{\n\t\t\"language\"\t\t\"english\"\n\t}\n}\n";
        var tmpFilePath = Path.GetTempFileName();
        File.WriteAllText(tmpFilePath, fileContent);
        var dir = Path.GetDirectoryName(tmpFilePath);
        var newRoute = Path.Combine(dir, FILE_NAME);
        
        File.Move(tmpFilePath, newRoute);
        _testFilePath = newRoute;
    }

    [TearDown]
    public void TearDown()
    {
        File.Delete(_testFilePath);
    }

    [Test]
    public async Task ParserTest()
    {
        /*//todo: arreglar mas adelante
        var parser = new SteamFilesParser();
        //Depende como tengas lleno de mierda este directorio, igual seria mejor guardarlo en otro lado
        var localGames = await parser.GetLocalGamesAsync(Path.GetTempPath());
        Assert.That(localGames, Is.Not.Null);*/
    }
}