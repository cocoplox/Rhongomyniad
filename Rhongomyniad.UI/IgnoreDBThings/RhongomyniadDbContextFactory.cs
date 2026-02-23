using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Rhongomyniad.Domain.Context;

namespace Rhongomyniad.UI.IgnoreDBThings
{
    public class RhongomyniadDbContextFactory : IDesignTimeDbContextFactory<RhongomyniadDbContext>
    {
        public RhongomyniadDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RhongomyniadDbContext>();

            // Aquí replicamos la lógica de dónde está la base de datos
            var appName = "Rhongomyniad"; // o config fija temporal
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(basePath,appName);
            Directory.CreateDirectory(dbPath);
            var dbFile = Path.Combine(dbPath, $"{appName}.db");

            optionsBuilder.UseSqlite($"Data Source={dbFile}");

            return new RhongomyniadDbContext(optionsBuilder.Options);
        }
    }
}