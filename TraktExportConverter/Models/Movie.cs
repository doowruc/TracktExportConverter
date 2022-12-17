using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class Movie
    {
        [JsonProperty("playDates")]
        public List<DateTimeOffset> PlayDates { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
