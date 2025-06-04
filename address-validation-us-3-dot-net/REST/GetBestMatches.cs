using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace address_validation_us_3_dot_net.REST
{
    /// <summary>
    /// Client for the AV3 GetBestMatches operation, providing both sync and async methods.
    /// Handles live vs. trial endpoints, URL encoding, fallback logic, and JSON deserialization.
    /// </summary>
    public static class GetBestMatchesClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AV3/api.svc/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AV3/api.svc/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AV3/api.svc/";

        /// <summary>
        /// Synchronously invoke the GetBestMatchesJson endpoint.
        /// </summary>
        /// <param name="input">Data for the request (address components, license key, isLive).</param>
        /// <returns>Deserialized <see cref="GBMResponse"/>.</returns>
        public static GBMResponse Invoke(GetBestMatchesInput input)
        {
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            var response = Helper.HttpGet<GBMResponse>(url, input.TimeoutSeconds);

            // Fallback on error payload in live mode
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallbackResponse = Helper.HttpGet<GBMResponse>(fallbackUrl, input.TimeoutSeconds);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously invoke the GetBestMatchesJson endpoint.
        /// </summary>
        /// <param name="input">Data for the request (address components, license key, isLive).</param>
        /// <returns>Deserialized <see cref="GBMResponse"/>.</returns>
        public static async Task<GBMResponse> InvokeAsync(GetBestMatchesInput input)
        {
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            var response = await Helper.HttpGetAsync<GBMResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallbackResponse = await Helper.HttpGetAsync<GBMResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        public record GetBestMatchesInput(
                                            string BusinessName,
                                            string Address,
                                            string Address2,
                                            string City,
                                            string State,
                                            string PostalCode,
                                            string LicenseKey,
                                            bool IsLive,
                                            int TimeoutSeconds = 15
        );

        // Build the full request URL, including URL-encoded query string
        private static string BuildUrl(GetBestMatchesInput input, string baseUrl)
        {
            var qs = $"GetBestMatchesJson?BusinessName={Helper.UrlEncode(input.BusinessName)}" +
                     $"&Address={Helper.UrlEncode(input.Address)}" +
                     $"&Address2={Helper.UrlEncode(input.Address2)}" +
                     $"&City={Helper.UrlEncode(input.City)}" +
                     $"&State={Helper.UrlEncode(input.State)}" +
                     $"&PostalCode={Helper.UrlEncode(input.PostalCode)}" +
                     $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GBMResponse response) => response?.Error == null || response.Error.TypeCode != "3";
    }
}
