using Newtonsoft.Json;

namespace TraktExportConverter.Models
{
    public class Show : TraktItemBase
    {
        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }
    }
}
