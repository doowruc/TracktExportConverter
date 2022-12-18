using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class Movie : TraktItemBase
    {
        [JsonProperty("playDates")]
        public List<DateTimeOffset> PlayDates { get; set; }
    }
}
