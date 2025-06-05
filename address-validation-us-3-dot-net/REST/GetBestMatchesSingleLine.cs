using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace address_validation_us_3_dot_net.REST
{
    /// <summary>
    /// Client for the AV3 GetBestMatchesSingleLine operation,
    /// which validates a single-line address string and returns parsed candidates.
    /// Includes sync/async methods, environment switching, URL encoding, and fallback logic.
    /// </summary>
    public static class GetBestMatchesSingleLineClient
    {
        // Base URL constants for production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AV3/api.svc/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AV3/api.svc/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AV3/api.svc/";

        /// <summary>
        /// Synchronously invoke the GetBestMatchesSingleLineJson endpoint.
        /// </summary>
        /// <param name="input">Request data container for single-line address lookup.</param>
        /// <returns>Deserialized <see cref="GBMResponse"/> with address candidates or error.</returns>
        public static GBMResponse Invoke(GetBestMatchesSingleLineInput input)
        {
            var baseUrl = input.IsLive ? LiveBaseUrl : TrialBaseUrl;
            var url = BuildUrl(input, baseUrl);

            var response = Helper.HttpGet<GBMResponse>(url, input.TimeoutSeconds);
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallback = Helper.HttpGet<GBMResponse>(fallbackUrl, input.TimeoutSeconds);
                return IsValid(fallback) ? fallback : response;
            }
            return response;
        }

        /// <summary>
        /// Asynchronously invoke the GetBestMatchesSingleLineJson endpoint.
        /// </summary>
        /// <param name="input">Request data container for single-line address lookup.</param>
        /// <returns>Task resolving to deserialized <see cref="GBMResponse"/>.</returns>
        public static async Task<GBMResponse> InvokeAsync(GetBestMatchesSingleLineInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            var baseUrl = input.IsLive ? LiveBaseUrl : TrialBaseUrl;
            var url = BuildUrl(input, baseUrl);

            var response = await Helper.HttpGetAsync<GBMResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                var fallback = await Helper.HttpGetAsync<GBMResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return IsValid(fallback) ? fallback : response;
            }
            return response;
        }

        // Build the full request URL for the single-line match operation, url encoded query string.
        private static string BuildUrl(GetBestMatchesSingleLineInput input, string baseUrl)
        {
            var qb = $"GetBestMatchesSingleLineJson?BusinessName={Helper.UrlEncode(input.BusinessName)}" +
                     $"&Address={Helper.UrlEncode(input.Address)}" +
                     $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
            return baseUrl + qb;
        }

        private static bool IsValid(GBMResponse response) => response?.Error == null || response.Error.TypeCode != "3";
    }

    /// <summary>
    /// Input parameters for the GetBestMatchesSingleLine operation.
    /// </summary>
    /// <param name="BusinessName">Company name to assist suite parsing (e.g., "Acme Corp"). - Optional</param>
    /// <param name="Address">Full single-line address to validate (e.g., "123 Main St Anytown CA 90012"). - Required</param>
    /// <param name="LicenseKey">Service Objects AV3 license key. - Required</param>
    /// <param name="IsLive">True for live (production+backup) endpoints; false for trial only. - Required</param>
    /// <param name="TimeoutSeconds">Request timeout in seconds (default: 15).</param>
    public record GetBestMatchesSingleLineInput(
        string BusinessName = "",
        string Address = "",
        string LicenseKey = "",
        bool IsLive = true,
        int TimeoutSeconds = 15
    );
}
