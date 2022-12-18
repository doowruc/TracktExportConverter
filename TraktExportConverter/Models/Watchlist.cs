using Newtonsoft.Json;

namespace TraktExportConverter.Models;

public class Watchlist
{
    [JsonProperty("listed_at")]
    public DateTimeOffset ListedAt { get; set; }

    [JsonProperty("show")]
    public Show Show { get; set; }

    [JsonProperty("movie")]
    public Movie Movie { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("rank")]
    public long Rank { get; set; }
}
