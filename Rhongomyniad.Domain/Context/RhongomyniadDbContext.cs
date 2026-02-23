using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Context;

public partial class RhongomyniadDbContext : DbContext
{
    public RhongomyniadDbContext()
    {
    }

    public RhongomyniadDbContext(DbContextOptions<RhongomyniadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LocalGame> LocalGames { get; set; }

    public virtual DbSet<SteamSetting> SteamSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocalGame>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("local_games");

            entity.Property(e => e.AppId)
                .HasColumnType("BIGINT")
                .HasColumnName("app_id");
            entity.Property(e => e.ConfigFilesDir).HasColumnName("config_files_dir");
            entity.Property(e => e.GameLauncher).HasColumnName("game_launcher");
            entity.Property(e => e.InstallDir).HasColumnName("install_dir");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SaveFilesDir).HasColumnName("save_files_dir");
        });

        modelBuilder.Entity<SteamSetting>(entity =>
        {
            entity.ToTable("steam_settings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.InstallPath).HasColumnName("install_path");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME")
                .HasColumnName("last_updated");
            entity.Property(e => e.LibraryFolders).HasColumnName("library_folders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
