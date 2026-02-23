using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rhongomyniad.Domain.Context;

namespace Rhongomyniad.Tests.Repositories;

[TestFixture]
public class ContextTest
{
    private SqliteConnection _connection;
    private RhongomyniadDbContext _context;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<RhongomyniadDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new RhongomyniadDbContext(options);
        _context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _connection.Dispose();
    }

    [Test]
    public void ShouldParseListToJsonArray()
    {
        List<string> testsPath = new ()
        {
            @"C:\Test1",
            @"C:\Test2"
        };
        _context.SteamSettings.Add(new() {Id = 1,LibraryFolders = testsPath, InstallPath = "", LastUpdated =  DateTime.Now});
        _context.SaveChanges();
        
        var tests = _context.SteamSettings.ToList().FirstOrDefault();
        Assert.That(tests, Is.Not.Null);
        Assert.That(tests.LibraryFolders.Count() > 1);
    }
}