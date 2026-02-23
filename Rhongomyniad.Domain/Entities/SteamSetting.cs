using System;
using System.Collections.Generic;

namespace Rhongomyniad.Domain.Entities;

public partial class SteamSetting
{
    public int Id { get; set; }

    public string InstallPath { get; set; } = null!;

    public List<string> LibraryFolders { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }
}
