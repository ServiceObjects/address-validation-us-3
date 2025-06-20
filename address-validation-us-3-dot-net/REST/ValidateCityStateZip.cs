﻿using System.Threading.Tasks;
using static address_validation_us_3_dot_net.REST.GetSecondaryNumbersClient;

namespace address_validation_us_3_dot_net.REST
{
    /// <summary>
    /// Client for the AV3 ValidateCityStateZip operation, which ensures
    /// a city, state, and zip combination is valid and returns corrected
    /// values when inputs are marginally incorrect.
    /// Provides synchronous and asynchronous methods with fallback logic.
    /// </summary>
    public static class ValidateCityStateZipClient
    {
        // Base URL constants for production, backup, and trial environments
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AV3/api.svc/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AV3/api.svc/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AV3/api.svc/";

        /// <summary>
        /// Synchronously invoke the CityStateZipInfo endpoint to validate and correct
        /// a city-state-zip combination.
        /// </summary>
        /// <param name="input">Request data container for city/state/zip lookup.</param>
        /// <returns>Deserialized <see cref="CSZResponse"/> with corrected values or error.</returns>
        public static CSZResponse Invoke(ValidateCityStateZipInput input)
        {
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            CSZResponse response = Helper.HttpGet<CSZResponse>(url, input.TimeoutSeconds);

            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                CSZResponse fallbackResponse = Helper.HttpGet<CSZResponse>(fallbackUrl, input.TimeoutSeconds);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }
            return response;
        }

        /// <summary>
        /// Asynchronously invoke the CityStateZipInfo endpoint to validate and correct
        /// a city-state-zip combination.
        /// </summary>
        /// <param name="input">Request data container for city/state/zip lookup.</param>
        /// <returns>Task resolving to deserialized <see cref="CSZResponse"/>.</returns>
        public static async Task<CSZResponse> InvokeAsync(ValidateCityStateZipInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            CSZResponse response = await Helper.HttpGetAsync<CSZResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                CSZResponse fallbackResponse = await Helper.HttpGetAsync<CSZResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }
            return response;
        }

        // Build the full request URL with URL-encoded query string
        private static string BuildUrl(ValidateCityStateZipInput input, string baseUrl)
        {
            var qs = $"ValidateCityStateZipJson?City={Helper.UrlEncode(input.City)}" +
                     $"&State={Helper.UrlEncode(input.State)}" +
                     $"&PostalCode={Helper.UrlEncode(input.Zip)}" +
                     $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        /// <summary>
        /// Simple validation: response must have no error and at least one corrected combo.
        /// </summary>
        private static bool IsValid(CSZResponse response) => response?.Error == null || response.Error.TypeCode != "3";

        /// <summary>
        /// Input parameters for the ValidateCityStateZip operation.
        /// </summary>
        /// <param name="City">City name to validate.- Required when PostalCode is missing.</param>
        /// <param name="State">State code or name to validate. - Required</param>
        /// <param name="Zip">ZIP code to validate. - Required when City and state are missing.</param>
        /// <param name="LicenseKey">Service Objects AV3 license key. - Required</param>
        /// <param name="IsLive">True for live (production+backup) endpoints; false for trial only. - Required</param>
        /// <param name="TimeoutSeconds">Timeout in seconds for HTTP calls (default 15).</param>
        public record ValidateCityStateZipInput(
            string City = "",
            string State = "",
            string Zip = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}
