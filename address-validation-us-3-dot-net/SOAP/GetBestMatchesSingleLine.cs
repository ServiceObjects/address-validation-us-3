using System;
using AV3Service;

namespace address_validation_us_3_dot_net.SOAP
{
    /// <summary>
    /// A simple wrapper class to call the AV3 GetBestMatchesSingleLine SOAP 
    /// operation endpoint (with primary/backup failover and an “IsLive” toggle).
    /// </summary>
    public class GetBestMatchesSingleLineValidation
    {
        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Reads configuration keys and initializes URLs/timeout/IsLive.
        /// </summary>
        public GetBestMatchesSingleLineValidation()
        {
            // Read timeout (milliseconds) and IsLive flag
            _timeoutMs = 10000;
            _isLive = false;

            // Depending on IsLive, pick the correct appSettings keys
            if (_isLive)
            {
                _primaryUrl = "https://sws.serviceobjects.com/av3/api.svc/soap";
                _backupUrl = "https://swsbackup.serviceobjects.com/av3/api.svc/soap";
            }
            else
            {
                _primaryUrl = "https://trial.serviceobjects.com/av3/api.svc/soap";
                _backupUrl = "https://trial.serviceobjects.com/av3/api.svc/soap";
            }

            if (string.IsNullOrWhiteSpace(_primaryUrl))
                throw new InvalidOperationException("Primary URL not set. Check appSettings (AV3_PRIMARY...).");
            if (string.IsNullOrWhiteSpace(_backupUrl))
                throw new InvalidOperationException("Backup URL not set. Check appSettings (AV3_BACKUP...).");
        }

        /// <summary>
        /// Calls the GetBestMatchesSingleLine SOAP operation. If the primary endpoint returns null
        /// or a fatal Error.TypeCode == "3", this will fall back to the backup endpoint.
        /// </summary>
        /// <param name="businessName">Business name (or empty if none)</param>
        /// <param name="fullAddress">Full address: Address, City, State, ZIP</param>
        /// <param name="licenseKey">Your ServiceObjects Address Validation US 3 license key</param>
        /// <returns>A BestMatchesResponse containing an array of Address objects (or an Error)</returns>
        /// <exception cref="Exception">
        /// Thrown if both primary and backup endpoints fail.
        /// </exception>
        public BestMatchesResponse GetBestMatchesSingleLine(
            string businessName,
            string fullAddress,
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

                var response = clientPrimary.GetBestMatchesSingleLineAsync(
                    businessName,
                    fullAddress,
                    licenseKey
                ).Result;

                // If the response is null, or if a “fatal” Error.TypeCode == "3" came back, force a fallback
                if (response == null || (response.Error != null && response.Error.TypeCode == "3"))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for GetBestMatchesSingleLine.");
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

                    return clientBackup.GetBestMatchesSingleLineAsync(
                        businessName,
                        fullAddress,
                        licenseKey
                    ).Result;
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
