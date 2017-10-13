using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace Infrastructure.Services.Common
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            const string bgCulture = "bg-BG";
            const string decimalSeparator = ".";

            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo(bgCulture) { NumberFormat = new NumberFormatInfo() { CurrencyDecimalSeparator = decimalSeparator, NumberDecimalSeparator = decimalSeparator } },
            };

            var requestCulture = new RequestCulture(culture: bgCulture, uiCulture: bgCulture);
            requestCulture.Culture.NumberFormat.CurrencyDecimalSeparator = decimalSeparator;
            requestCulture.Culture.NumberFormat.NumberDecimalSeparator = decimalSeparator;

            return app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = requestCulture,
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });
        }
    }
}
