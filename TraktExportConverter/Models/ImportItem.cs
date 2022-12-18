namespace TraktExportConverter.Models
{
    public class ImportItem
    {
        public ImportItem(string title, int year, int season, int episode, DateTimeOffset playDate)
        {
            Type = TraktType.Show.ToString();
            Title = $"{GetTitle(title, year)} s{season:D2}e{episode:D2}";
            Date = playDate;
        }

        public ImportItem(string title, int year, DateTimeOffset playDate)
        {
            Type = TraktType.Movie.ToString();
            Title = GetTitle(title, year);
            Date = playDate;
        }

        public ImportItem(string title, int year, TraktType type, DateTimeOffset listedAt)
        {
            Type = $"{TraktType.Watchlist} {type}";
            Title = GetTitle(title, year);
            Date = listedAt;
        }

        public string Type { get; set; }

        public string Title { get; private set; }

        public DateTimeOffset Date { get; private set; }

        public string DateDisplay => $"<time data-value=\"{Date:O}\">{Date:O}</time>";

        private static string GetTitle(string title, int year) => $"{title} ({year})";
    }
}
