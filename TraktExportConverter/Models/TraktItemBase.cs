using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class TraktItemBase
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
