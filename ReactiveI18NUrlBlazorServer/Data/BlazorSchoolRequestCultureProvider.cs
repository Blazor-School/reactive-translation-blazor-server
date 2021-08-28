using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Threading.Tasks;

namespace ReactiveI18NUrlBlazorServer.Data
{
    public class BlazorSchoolRequestCultureProvider : RequestCultureProvider
    {
        private readonly BlazorSchoolLocalizationOptions _localizationOptions;
        private string _selectedLanguage { get; set; }

        public BlazorSchoolRequestCultureProvider(BlazorSchoolLocalizationOptions localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext.Request.Headers["Sec-Fetch-Dest"] == "document")
            {
                var query = httpContext.Request.Query;
                string selectedLanguage = query["language"].ToString() ?? _localizationOptions.DefaultLanguage;
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(selectedLanguage);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(selectedLanguage);
                var result = new ProviderCultureResult(selectedLanguage);

                _selectedLanguage = selectedLanguage;

                return Task.FromResult(result);
            }
            else
            {
                return Task.FromResult(new ProviderCultureResult(_selectedLanguage));
            }
        }
    }
}
