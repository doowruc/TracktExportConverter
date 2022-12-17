namespace TraktExportConverter.Models.WatchedShows
{
    public class WatchedShow
    {
        public WatchedShow(string title, int year, int season, int episode, DateTimeOffset watched)
        {
            Show = $"{title} ({year})";
            Episode = $"s{season:D2}e{episode:D2}";
            Watched = watched;
        }

        public string Show { get; set; }
        public string Episode { get; set; }
        public DateTimeOffset Watched { get; set; }
        public string WatchedDisplay => $"<time data-value=\"{Watched:O}\"></time>";
    }
}
