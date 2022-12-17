using Microsoft.AspNetCore.Mvc;
using TraktExportConverter.Models;

namespace TraktExportConverter.Pages
{
    public class WatchedShowsModel : BasePageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostImport()
        {
            var (result, shows) = await Import<Show>();

            if (result != null)
            {
                return result;
            }

            var watchedShows = (from show in shows
                                from season in show.Seasons
                                from episode in season.Episodes
                                from playDate in episode.PlayDates
                                select new WatchedShow(show.Title, show.Year, season.Number, episode.Number, playDate))
                .ToList();

            return Success(watchedShows);
        }
    }
}
