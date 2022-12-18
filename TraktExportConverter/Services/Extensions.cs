using Microsoft.AspNetCore.Mvc.Rendering;

namespace TraktExportConverter.Services
{
    public static class Extensions
    {
        public static string NavLinkTextClass(this ViewContext viewContext, string value) =>
            Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName) == value ? "active fw-bold" : "";
    }
}
