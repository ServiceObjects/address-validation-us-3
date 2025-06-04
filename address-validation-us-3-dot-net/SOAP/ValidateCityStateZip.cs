using System;
using AV3Service;

namespace address_validation_us_3_dot_net.SOAP
{
    /// <summary>
    /// A simple wrapper class to call the AV3 ValidateCityStateZip SOAP 
    /// operation endpoint (with primary/backup failover and an “IsLive” toggle).
    /// </summary>
    public class CityStateZipValidation
    {
        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Reads configuration keys and initializes URLs/timeout/IsLive.
        /// </summary>
        public CityStateZipValidation()
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
        /// Calls the ValidateCityStateZip SOAP operation. If the primary endpoint returns null
        /// or a fatal Error.TypeCode == "3", this will fall back to the backup endpoint.
        /// </summary>
        /// <param name="city">City</param>
        /// <param name="state">State (2‑letter or full state name)</param>
        /// <param name="zip">ZIP or ZIP+4</param>
        /// <param name="licenseKey">Your ServiceObjects Address Validation US 3 license key</param>
        /// <returns>A CityStateZipResponse containing CityStateZip object (or an Error)</returns>
        /// <exception cref="Exception">
        /// Thrown if both primary and backup endpoints fail.
        /// </exception>
        public CityStateZipResponse ValidateCityStateZip(
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

                var response = clientPrimary.ValidateCityStateZipAsync(
                    city,
                    state,
                    zip,
                    licenseKey
                ).Result;

                // If the response is null, or if a “fatal” Error.TypeCode == "3" came back, force a fallback
                if (response == null || (response.Error != null && response.Error.TypeCode == "3"))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for ValidateCityStateZip.");
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

                    return clientBackup.ValidateCityStateZipAsync(
                        city,
                        state,
                        zip,
                        licenseKey
                    ).Result;
                }
                catch (Exception backupEx)
                {
                    // If backup also fails, wrap both exceptions
                    throw new Exception(
                        $"Both primary and backup endpoints for ValidateCityStateZip failed.\n" +
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
