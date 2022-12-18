namespace TraktExportConverter.Models
{
    public class Watched
    {
        public Watched(string title, int year, int season, int episode, DateTimeOffset playDate)
        {
            Title = $"{title} ({year}) s{season:D2}e{episode:D2}";
            PlayDate = playDate;
        }

        public Watched(string title, int year, DateTimeOffset playDate)
        {
            Title = $"{title} ({year})";
            PlayDate = playDate;
        }

        public string Title { get; private set; }
        public DateTimeOffset PlayDate { get; private set; }
        public string PlayDateDisplay => $"<time data-value=\"{PlayDate:O}\">{PlayDate:O}</time>";
    }
}
