namespace TraktExportConverter.Models
{
    public class WatchedShow : WatchedBase
    {
        public WatchedShow(string title, int year, int season, int episode, DateTimeOffset watched)
            : base(title, year, watched)
        {
            Episode = $"s{season:D2}e{episode:D2}";
        }

        public string Episode { get; set; }
    }
}
