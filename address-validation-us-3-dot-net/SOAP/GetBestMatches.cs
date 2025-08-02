using AV3Service;
using System;
using System.Threading.Tasks;

namespace address_validation_us_3_dot_net.SOAP
{
    /// <summary>
    /// A simple wrapper class to call the AV3 GetBestMatches SOAP 
    /// operation endpoint (with primary/backup failover and an “IsLive” toggle).
    /// </summary>
    public class GetBestMatchesValidation
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/av3/api.svc/soap";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/av3/api.svc/soap";
        private const string TrailBaseUrl = "https://trial.serviceobjects.com/av3/api.svc/soap";

        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Initializes URLs/timeout/IsLive.
        /// </summary>
        public GetBestMatchesValidation(bool IsLive)
        {
            // Read timeout (milliseconds) and IsLive flag
            _timeoutMs = 10000;
            _isLive = IsLive;

            if (_isLive)
            {
                _primaryUrl = LiveBaseUrl;
                _backupUrl = BackupBaseUrl;
            }
            else
            {
                _primaryUrl = TrailBaseUrl;
                _backupUrl = TrailBaseUrl;
            }

            if (string.IsNullOrWhiteSpace(_primaryUrl))
                throw new InvalidOperationException("Primary URL not set. Check endpoint configuration.");

            if (string.IsNullOrWhiteSpace(_backupUrl))
                throw new InvalidOperationException("Backup URL not set. Check endpoint configuration.");
        }

        /// <summary>
        /// Calls the GetBestMatches SOAP operation. If the primary endpoint returns null
        /// or a fatal Error.TypeCode == "3", this will fall back to the backup endpoint.
        /// </summary>
        /// <param name="businessName">Business name (or empty if none)</param>
        /// <param name="address1">Street address line 1</param>
        /// <param name="address2">Street address line 2 (or empty)</param>
        /// <param name="city">City</param>
        /// <param name="state">State (2‑letter or full state name)</param>
        /// <param name="zip">ZIP or ZIP+4</param>
        /// <param name="licenseKey">Your ServiceObjects Address Validation US 3 license key</param>
        /// <returns>A BestMatchesResponse containing an array of Address objects (or an Error)</returns>
        /// <exception cref="Exception">
        /// Thrown if both primary and backup endpoints fail.
        /// </exception>
        public async Task<BestMatchesResponse> GetBestMatches(
            string businessName,
            string address1,
            string address2,
            string city,
            string state,
            string zip,
            string licenseKey
        )
        {
            AddressValidation3Client clientPrimary = null;
            AddressValidation3Client clientBackup = null;

            try
            {
                // 1) Attempt Primary
                clientPrimary = new AddressValidation3Client();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                BestMatchesResponse response = await clientPrimary.GetBestMatchesAsync(
                    businessName,
                    address1,
                    address2,
                    city,
                    state,
                    zip,
                    licenseKey
                ).ConfigureAwait(false);

                // If the response is null, or if a “fatal” Error.TypeCode == "3" came back, force a fallback
                if (response == null || (response.Error != null && response.Error.TypeCode == "3"))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for GetBestMatches.");
                }

                return response;
            }
            catch (Exception primaryEx)
            {
                // 2) Primary failed (due to exception, null, or TypeCode=3). Try Backup.
                try
                {
                    clientBackup = new AddressValidation3Client();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.GetBestMatchesAsync(
                        businessName,
                        address1,
                        address2,
                        city,
                        state,
                        zip,
                        licenseKey
                    ).ConfigureAwait(false);
                }
                catch (Exception backupEx)
                {
                    // If backup also fails, wrap both exceptions
                    throw new Exception(
                        $"Both primary and backup endpoints failed.\n" +
                        $"Primary error: {primaryEx.Message}\n" +
                        $"Backup error: {backupEx.Message}"
                    );
                }
                finally
                {
                    clientBackup?.Close();
                }
            }
            finally
            {
                clientPrimary?.Close();
            }
        }
    }
}
