using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace address_validation_us_3_dot_net.REST
{
    /// <summary>
    /// Client for the AV3 GetSecondaryNumbers operation, providing both sync and async methods.
    /// Handles live vs. trial endpoints, URL encoding, fallback logic, and JSON deserialization.
    /// </summary>
    public static class GetSecondaryNumbersClient
    {
        // Base URL constants
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AV3/api.svc/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AV3/api.svc/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AV3/api.svc/";

        // Shared HttpClient for efficiency
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// Synchronously invoke the GetSecondaryNumbersJson endpoint.
        /// </summary>
        /// <param name="input">Request data container.</param>
        /// <returns>Deserialized <see cref="GSNResponse"/>.</returns>
        public static GSNResponse Invoke(GetSecondaryNumbersInput input)
        {
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            var response = Helper.HttpGet<GSNResponse>(url, input.TimeoutSeconds);

            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallbackResponse = Helper.HttpGet<GSNResponse>(fallbackUrl, input.TimeoutSeconds);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously invoke the GetSecondaryNumbersJson endpoint.
        /// </summary>
        /// <param name="input">Request data container.</param>
        /// <returns>Deserialized <see cref="GSNResponse"/>.</returns>
        public static async Task<GSNResponse> InvokeAsync(GetSecondaryNumbersInput input)
        {
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            var response = await Helper.HttpGetAsync<GSNResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallbackResponse = await Helper.HttpGetAsync<GSNResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        public record GetSecondaryNumbersInput(
                                                string Address,
                                                string City,
                                                string State,
                                                string PostalCode,
                                                string LicenseKey,
                                                bool IsLive,
                                                int TimeoutSeconds = 15
        );

        // Build the full request URL with URL-encoded query string
        private static string BuildUrl(GetSecondaryNumbersInput input, string baseUrl)
        {
            var qs = $"GetSecondaryNumbersJson?Address={Helper.UrlEncode(input.Address)}" +
                     $"&City={Helper.UrlEncode(input.City)}" +
                     $"&State={Helper.UrlEncode(input.State)}" +
                     $"&PostalCode={Helper.UrlEncode(input.PostalCode)}" +
                     $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GSNResponse response) => response?.Error == null || response.Error.TypeCode != "3";
    }
}
