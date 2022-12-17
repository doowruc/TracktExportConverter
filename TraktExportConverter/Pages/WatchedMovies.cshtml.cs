using Microsoft.AspNetCore.Mvc;
using TraktExportConverter.Models;

namespace TraktExportConverter.Pages
{
    public class WatchedMoviesModel : BasePageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostImport()
        {
            var (result, movies) = await Import<Movie>();

            if (result != null)
            {
                return result;
            }

            var watchedMovies = (from movie in movies
                                 from playDate in movie.PlayDates
                                 select new WatchedMovie(movie.Title, movie.Year, playDate))
                .ToList();

            return Success(watchedMovies);
        }
    }
}
