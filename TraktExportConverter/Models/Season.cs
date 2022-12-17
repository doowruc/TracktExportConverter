using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class Season
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }
}
