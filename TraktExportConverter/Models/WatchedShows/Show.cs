using Newtonsoft.Json;

namespace TraktExportConverter.Models.WatchedShows
{
    public class Show
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
