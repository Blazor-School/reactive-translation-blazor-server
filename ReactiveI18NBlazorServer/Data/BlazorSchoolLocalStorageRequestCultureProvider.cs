using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveI18NCookiesBlazorServer.Data
{
    public class BlazorSchoolLocalStorageRequestCultureProvider : RequestCultureProvider
    {
        public string DefaultCulture { get; set; }

        public BlazorSchoolLocalStorageRequestCultureProvider(string defaultCulture)
        {
            DefaultCulture = defaultCulture;
        }

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string inputCulture = httpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            var result = CookieRequestCultureProvider.ParseCookieValue(inputCulture);

            if (result is null)
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(DefaultCulture);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(DefaultCulture);
                result = new(DefaultCulture);
            }
            else
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(result.Cultures.First().Value);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(result.UICultures.First().Value);
            }

            return Task.FromResult(result);
        }
    }
}
