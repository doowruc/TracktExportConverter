namespace TraktExportConverter.Models
{
    public class WatchedMovie : WatchedBase
    {
        public WatchedMovie(string title, int year, DateTimeOffset watched)
            : base(title, year, watched)
        {
        }
    }
}
