using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TraktExportConverter.Models;

namespace TraktExportConverter.Pages
{
    public class WatchedModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public IFormFile? ImportFile { get; set; }

        [BindProperty]
        public string? ImportType { get; set; }

        public async Task<IActionResult> OnPostImport()
        {
            if (ImportFile == null || string.IsNullOrEmpty(ImportType))
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
                    "shows" => GetShows(json),
                    "movies" => GetMovies(json),
                    _ => null
                };

                return items == null ? Fail() : Success(items);
            }
            catch (Exception e)
            {
                // TODO: Log

                return Fail(e.Message);
            }
        }

        private List<Watched>? GetShows(string json)
        {
            var shows = JsonConvert.DeserializeObject<List<Show>>(json);

            return shows == null || !shows.Any()
                ? null
                : (from show in shows
                   from season in show.Seasons
                   from episode in season.Episodes
                   from playDate in episode.PlayDates
                   select new Watched(show.Title, show.Year, season.Number, episode.Number, playDate))
                .ToList();
        }

        private List<Watched>? GetMovies(string json)
        {
            var movies = JsonConvert.DeserializeObject<List<Movie>>(json);

            return movies == null || !movies.Any()
                ? null
                : (from movie in movies
                   from playDate in movie.PlayDates
                   select new Watched(movie.Title, movie.Year, playDate))
                .ToList();
        }

        private JsonResult Fail(string? error = null) => new(new { success = false, error = error ?? "Failed to import" });

        private JsonResult Success(object data) => new(new { success = true, data });
    }
}
