using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TraktExportConverter.Models;

namespace TraktExportConverter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private const string ErrorMessage = "Failed to import";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IFormFile? ImportFile { get; set; }

        [BindProperty]
        public TraktType ImportType { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostImport()
        {
            if (ImportFile == null)
            {
                return Fail();
            }

            try
            {
                await using var ms = new MemoryStream();

                await ImportFile.CopyToAsync(ms);

                var json = Encoding.UTF8.GetString(ms.ToArray());

                if (string.IsNullOrEmpty(json))
                {
                    return Fail();
                }

                var items = ImportType switch
                {
                    TraktType.Show => GetShows(json),
                    TraktType.Movie => GetMovies(json),
                    TraktType.Watchlist => GetWatchlist(json),
                    _ => null
                };

                return items == null ? Fail() : Success(items);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ErrorMessage);

                return Fail();
            }
        }

        private List<ImportItem>? GetShows(string json)
        {
            var shows = JsonConvert.DeserializeObject<List<Show>>(json);

            return shows == null || !shows.Any()
                ? null
                : (from show in shows
                   from season in show.Seasons
                   from episode in season.Episodes
                   from playDate in episode.PlayDates
                   select new ImportItem(show.Title, show.Year, season.Number, episode.Number, playDate))
                .ToList();
        }

        private List<ImportItem>? GetMovies(string json)
        {
            var movies = JsonConvert.DeserializeObject<List<Movie>>(json);

            return movies == null || !movies.Any()
                ? null
                : (from movie in movies
                   from playDate in movie.PlayDates
                   select new ImportItem(movie.Title, movie.Year, playDate))
                .ToList();
        }

        private List<ImportItem>? GetWatchlist(string json)
        {
            var watchlist = JsonConvert.DeserializeObject<List<Watchlist>>(json);

            return watchlist == null || !watchlist.Any()
                ? null
                : (from item in watchlist
                   let type = item.Type == "movie" ? TraktType.Movie : TraktType.Show
                   let title = type == TraktType.Movie ? item.Movie.Title : item.Show.Title
                   let year = type == TraktType.Movie ? item.Movie.Year : item.Show.Year
                   select new ImportItem(title, year, type, item.ListedAt))
                .ToList();
        }

        private JsonResult Fail() => new(new { success = false, error = ErrorMessage });

        private JsonResult Success(object data) => new(new { success = true, data });
    }
}
