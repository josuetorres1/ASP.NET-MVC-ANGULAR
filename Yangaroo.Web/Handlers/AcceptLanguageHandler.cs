using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Yangaroo.Web.Handlers
{
    public class AcceptLanguageHandler : DelegatingHandler
    {
        private static readonly CultureInfo EnglishCultureInfo = CultureInfo.GetCultureInfo("en-CA");
        private static readonly CultureInfo FrenchCultureInfo = CultureInfo.GetCultureInfo("fr-CA");

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var cultureInfo = GetBestLanguage(request.Headers.AcceptLanguage);

            if (cultureInfo == null)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Accept Language is not valid")
                };

                return Task.FromResult(responseMessage);
            }

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            return base.SendAsync(request, cancellationToken);
        }

        private static CultureInfo GetBestLanguage(ICollection<StringWithQualityHeaderValue> languages)
        {
            if (!languages.Any())
            {
                return EnglishCultureInfo;
            }

            foreach (var language in languages.OrderByDescending(x => x.Quality ?? 1.0))
            {
                if (language.Value.StartsWith("en", StringComparison.OrdinalIgnoreCase))
                {
                    return EnglishCultureInfo;
                }

                if (language.Value.StartsWith("fr", StringComparison.OrdinalIgnoreCase))
                {
                    return FrenchCultureInfo;
                }

                try
                {
                    return CultureInfo.GetCultureInfo(language.Value);
                }
                catch (CultureNotFoundException)
                {

                }
            }

            return null;
        }
    }
}
