namespace TraktExportConverter.Models
{
    public abstract class WatchedBase
    {
        protected WatchedBase(string title, int year, DateTimeOffset watched)
        {
            Title = $"{title} ({year})";
            Watched = watched;
        }

        public string Title { get; private set; }
        public DateTimeOffset Watched { get; private set; }
        public string WatchedDisplay => $"<time data-value=\"{Watched:O}\"></time>";
    }
}
