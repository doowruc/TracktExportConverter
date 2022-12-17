using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TraktExportConverter.Models.WatchedShows;

namespace TraktExportConverter.Pages
{
    public class WatchedShowsModel : PageModel
    {
        [BindProperty]
        public IFormFile? ImportFile { get; set; }

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

                var shows = JsonConvert.DeserializeObject<List<Show>>(json);

                if (shows == null || !shows.Any())
                {
                    return Fail();
                }

                var history = (from show in shows
                               from season in show.Seasons
                               from episode in season.Episodes
                               from playDate in episode.PlayDates
                               select new WatchedShow(show.Title, show.Year, season.Number, episode.Number, playDate))
                    .ToList();

                return Success(history);
            }
            catch (Exception e)
            {
                // TODO: Log

                return Fail(e.Message);
            }
        }

        private JsonResult Fail(string? error = null) => new(new { success = false, error = error ?? "Failed to import" });

        private JsonResult Success(object data) => new(new { success = true, data });
    }
}
