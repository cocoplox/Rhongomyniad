using System;
using System.Collections.Generic;

namespace Rhongomyniad.Domain.Entities;

public partial class LocalGame
{
    public long AppId { get; set; }

    public string Name { get; set; } = null!;

    public string InstallDir { get; set; } = null!;

    public string GameLauncher { get; set; } = null!;

    public string? SaveFilesDir { get; set; }

    public string? ConfigFilesDir { get; set; }
}
