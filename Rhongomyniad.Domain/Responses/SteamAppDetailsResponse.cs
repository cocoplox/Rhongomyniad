using System.Text.Json;

namespace Rhongomyniad.Domain.Responses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class SteamAppDetailsResponse
{
    // La respuesta viene como:
    // { "440": { success: true, data: {...} } }
    // Por eso usamos diccionario
    [JsonExtensionData]
    public Dictionary<string, object> Raw { get; set; }

    // Opción tipada:
    public Dictionary<string, SteamAppDetails> Apps { get; set; }
}

public class SteamAppDetails
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public SteamAppData Data { get; set; }
}

public class SteamAppData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("steam_appid")]
    public int SteamAppId { get; set; }

    [JsonPropertyName("required_age")]
    public JsonElement RequiredAgeRaw { get; set; }

    [JsonIgnore]
    public int RequiredAge
    {
        get
        {
            if (RequiredAgeRaw.ValueKind == JsonValueKind.Number)
                return RequiredAgeRaw.GetInt32();
            if (RequiredAgeRaw.ValueKind == JsonValueKind.String && int.TryParse(RequiredAgeRaw.GetString(), out var value))
                return value;
            return 0; // default
        }
    }

    [JsonPropertyName("is_free")]
    public bool IsFree { get; set; }

    [JsonPropertyName("detailed_description")]
    public string DetailedDescription { get; set; }

    [JsonPropertyName("about_the_game")]
    public string AboutTheGame { get; set; }

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; }

    [JsonPropertyName("supported_languages")]
    public string SupportedLanguages { get; set; }

    [JsonPropertyName("header_image")]
    public string HeaderImage { get; set; }

    [JsonPropertyName("website")]
    public string Website { get; set; }

    [JsonPropertyName("developers")]
    public List<string> Developers { get; set; }

    [JsonPropertyName("publishers")]
    public List<string> Publishers { get; set; }

    [JsonPropertyName("platforms")]
    public SteamPlatforms Platforms { get; set; }

    [JsonPropertyName("genres")]
    public List<SteamGenre> Genres { get; set; }

    [JsonPropertyName("categories")]
    public List<SteamCategory> Categories { get; set; }

    [JsonPropertyName("screenshots")]
    public List<SteamScreenshot> Screenshots { get; set; }

    [JsonPropertyName("movies")]
    public List<SteamMovie> Movies { get; set; }

    [JsonPropertyName("price_overview")]
    public SteamPriceOverview PriceOverview { get; set; }

    [JsonPropertyName("release_date")]
    public SteamReleaseDate ReleaseDate { get; set; }

    [JsonPropertyName("pc_requirements")]
    public SteamRequirements PcRequirements { get; set; }

    [JsonPropertyName("mac_requirements")]
    public JsonElement MacRequirementsRaw { get; set; }
    [JsonIgnore]
    public SteamRequirements MacRequirements
    {
        get
        {
            if (MacRequirementsRaw.ValueKind == JsonValueKind.Object)
                return MacRequirementsRaw.Deserialize<SteamRequirements>()!;
            return new SteamRequirements();
        }
    }

    [JsonPropertyName("linux_requirements")]
    public JsonElement LinuxRequirementsRaw { get; set; }

    [JsonIgnore]
    public SteamRequirements LinuxRequirements
    {
        get
        {
            if (LinuxRequirementsRaw.ValueKind == JsonValueKind.Object)
                return LinuxRequirementsRaw.Deserialize<SteamRequirements>()!;
            return new SteamRequirements();
        }
    }

    [JsonPropertyName("metacritic")]
    public SteamMetacritic Metacritic { get; set; }
}

public class SteamPlatforms
{
    [JsonPropertyName("windows")]
    public bool Windows { get; set; }

    [JsonPropertyName("mac")]
    public bool Mac { get; set; }

    [JsonPropertyName("linux")]
    public bool Linux { get; set; }
}

public class SteamGenre
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class SteamCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class SteamScreenshot
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("path_thumbnail")]
    public string PathThumbnail { get; set; }

    [JsonPropertyName("path_full")]
    public string PathFull { get; set; }
}

public class SteamMovie
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("webm")]
    public SteamMovieFormat Webm { get; set; }

    [JsonPropertyName("mp4")]
    public SteamMovieFormat Mp4 { get; set; }
}

public class SteamMovieFormat
{
    [JsonPropertyName("480")]
    public string Low { get; set; }

    [JsonPropertyName("max")]
    public string Max { get; set; }
}

public class SteamPriceOverview
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("initial")]
    public int Initial { get; set; }

    [JsonPropertyName("final")]
    public int Final { get; set; }

    [JsonPropertyName("discount_percent")]
    public int DiscountPercent { get; set; }

    [JsonPropertyName("initial_formatted")]
    public string InitialFormatted { get; set; }

    [JsonPropertyName("final_formatted")]
    public string FinalFormatted { get; set; }
}

public class SteamReleaseDate
{
    [JsonPropertyName("coming_soon")]
    public bool ComingSoon { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }
}

public class SteamRequirements
{
    [JsonPropertyName("minimum")]
    public string Minimum { get; set; }

    [JsonPropertyName("recommended")]
    public string Recommended { get; set; }
}

public class SteamMetacritic
{
    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
