using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class Episode
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("playDates")]
        public List<DateTimeOffset> PlayDates { get; set; }
    }
}
