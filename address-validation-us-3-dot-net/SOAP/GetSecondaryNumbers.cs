using AV3Service;
using System;
using System.Threading.Tasks;

namespace address_validation_us_3_dot_net.SOAP
{
    /// <summary>
    /// A wrapper class to call AV3 GetSecondaryNumbers SOAP 
    /// operation endpoint (with primary/backup failover and an “IsLive” toggle).
    /// </summary>
    public class GetSecondaryNumbersValidation
    {
        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Reads configuration keys and initializes URLs/timeout/IsLive.
        /// </summary>
        public GetSecondaryNumbersValidation(bool IsLive)
        {
            // Read timeout (milliseconds) and IsLive flag
            _timeoutMs = 10000;
            _isLive = IsLive;

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
        /// Calls the GetSecondaryNumbers SOAP operation. If the primary endpoint returns null
        /// or a fatal Error.TypeCode == "3", this will fall back to the backup endpoint.
        /// </summary>
        /// <param name="address1">Street address line 1</param>
        /// <param name="city">City</param>
        /// <param name="state">State (2‑letter or full state name)</param>
        /// <param name="zip">ZIP or ZIP+4</param>
        /// <param name="licenseKey">Your ServiceObjects Address Validation US 3 license key</param>
        /// <returns>A SecondaryNumbersResponse containing the address and array of SecondaryNumbers objects (or an Error)</returns>
        /// <exception cref="Exception">
        /// Thrown if both primary and backup endpoints fail.
        /// </exception>
        public async Task<SecondaryNumbersResponse> GetSecondaryNumbers(
            string address1,
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

                SecondaryNumbersResponse response = await clientPrimary.GetSecondaryNumbersAsync(
                    address1,
                    city,
                    state,
                    zip,
                    licenseKey
                ).ConfigureAwait(false);

                // If the response is null, or if a “fatal” Error.TypeCode == "3" came back, force a fallback
                if (response == null || (response.Error != null && response.Error.TypeCode == "3"))
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for GetSecondaryNumbers.");

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

                    return await clientBackup.GetSecondaryNumbersAsync(
                        address1,
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
