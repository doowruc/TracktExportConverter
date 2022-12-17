using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TraktExportConverter.Pages
{
    public abstract class BasePageModel : PageModel
    {
        [BindProperty]
        public IFormFile? ImportFile { get; set; }

        protected async Task<(IActionResult?, List<T>?)> Import<T>()
        {
            if (ImportFile == null)
            {
                return (Fail(), null);
            }

            try
            {
                await using var ms = new MemoryStream();

                await ImportFile.CopyToAsync(ms);

                var json = Encoding.UTF8.GetString(ms.ToArray());

                if (string.IsNullOrEmpty(json))
                {
                    return (Fail(), null);
                }

                var items = JsonConvert.DeserializeObject<List<T>>(json);

                return items == null || !items.Any()
                    ? (Fail(), null)
                    : (null, items);
            }
            catch (Exception e)
            {
                // TODO: Log

                return (Fail(e.Message), null);
            }
        }

        protected JsonResult Fail(string? error = null) => new(new { success = false, error = error ?? "Failed to import" });

        protected JsonResult Success(object data) => new(new { success = true, data });
    }
}
