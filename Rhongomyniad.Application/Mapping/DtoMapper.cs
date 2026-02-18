using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Application.Mapping;

public static class DtoMapper
{
    public static DTOs.GameSummaryDto ToSummaryDto(Game game)
    {
        ArgumentNullException.ThrowIfNull(game);

        return new DTOs.GameSummaryDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Publisher = game.Publisher,
            Developer = game.Developer,
            Launcher = game.Launcher,
            InstallPath = game.InstallPath.Value,
            LauncherGameId = game.LauncherGameId,
            HasSaveProfile = game.SaveProfile != null,
            HasConfigProfile = game.ConfigProfile != null,
            DetectedAt = game.DetectedAt,
            LastPlayedAt = game.LastPlayedAt
        };
    }

    public static IReadOnlyList<DTOs.GameSummaryDto> ToSummaryDtos(IEnumerable<Game> games)
    {
        ArgumentNullException.ThrowIfNull(games);
        return games.Select(ToSummaryDto).ToList().AsReadOnly();
    }
}
