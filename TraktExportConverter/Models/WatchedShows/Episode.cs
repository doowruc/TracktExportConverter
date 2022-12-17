using Newtonsoft.Json;

namespace TraktExportConverter.Models.WatchedShows
{
    public class Episode
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public int Number { get; set; }

        [JsonProperty("playDates")]
        public List<DateTimeOffset> PlayDates { get; set; }
    }
}
