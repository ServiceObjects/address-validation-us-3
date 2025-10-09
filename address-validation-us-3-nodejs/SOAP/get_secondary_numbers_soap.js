import { soap } from 'strong-soap';

/**
 * <summary>
 * A class that provides functionality to call the ServiceObjects Address Validation (AV3) SOAP service's GetSecondaryNumbers endpoint,
 * retrieving secondary address information (e.g., apartment or suite numbers) for a given U.S. address with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
class GetSecondaryNumbersSoap {
    /**
     * <summary>
     * Initializes a new instance of the GetSecondaryNumbersSoap class with the provided input parameters,
     * setting up primary and backup WSDL URLs based on the live/trial mode.
     * </summary>
     * @param {string} Address - Address line 1 of the address (e.g., "123 Main Street"). Required.
     * @param {string} City - The city of the address (e.g., "New York"). Required.
     * @param {string} State - The state of the address (e.g., "NY"). Required.
     * @param {string} PostalCode - The zip code of the address. Required.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @throws {Error} Thrown if LicenseKey is empty or null.
     */
    constructor(Address, City, State, PostalCode, LicenseKey, isLive = true, timeoutSeconds = 15) {

        this.args = {
            Address,
            City,
            State,
            PostalCode,
            LicenseKey
        };

        this.isLive = isLive;
        this.timeoutSeconds = timeoutSeconds;

        this.LiveBaseUrl = 'https://sws.serviceobjects.com/AV3/api.svc?wsdl';
        this.BackupBaseUrl = 'https://swsbackup.serviceobjects.com/AV3/api.svc?wsdl';
        this.TrialBaseUrl = 'https://trial.serviceobjects.com/AV3/api.svc?wsdl';

        this._primaryWsdl = this.isLive ? this.LiveBaseUrl : this.TrialBaseUrl;
        this._backupWsdl = this.isLive ? this.BackupBaseUrl : this.TrialBaseUrl;
    }

    /**
     * <summary>
     * Asynchronously calls the GetSecondaryNumbers SOAP endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.TypeCode == '3') in live mode
     * or if the primary call fails.
     * </summary>
     * @returns {Promise<Object>} A promise that resolves to the raw SOAP response data containing secondary address details or an error.
     * @throws {Error} Thrown if both primary and backup calls fail, with detailed error messages.
     */
    async invokeAsync() {
        try {
            const primaryResult = await this._callSoap(this._primaryWsdl, this.args);

            if (this.isLive && !this._isValid(primaryResult)) {
                console.warn("Primary returned Error.TypeCode == '3', falling back to backup...");
                const backupResult = await this._callSoap(this._backupWsdl, this.args);
                return backupResult;
            }

            return primaryResult;
        } catch (primaryErr) {
            try {
                console.warn("Primary call failed, falling back to backup...");
                const backupResult = await this._callSoap(this._backupWsdl, this.args);
                return backupResult;
            } catch (backupErr) {
                throw new Error(`Both primary and backup calls failed:\nPrimary: ${primaryErr.message}\nBackup: ${backupErr.message}`);
            }
        }
    }

    /**
     * <summary>
     * Performs a SOAP service call to the specified WSDL URL with the given arguments,
     * creating a client and returning the raw SOAP response data.
     * </summary>
     * @param {string} wsdlUrl - The WSDL URL of the SOAP service endpoint (primary or backup).
     * @param {Object} args - The arguments to pass to the GetSecondaryNumbers method.
     * @returns {Promise<Object>} A promise that resolves to the raw SOAP response data.
     * @throws {Error} Thrown if the SOAP client creation fails, the service call fails, or the response cannot be parsed.
     */
    _callSoap(wsdlUrl, args) {
        return new Promise((resolve, reject) => {
            soap.createClient(wsdlUrl, { timeout: this.timeoutSeconds * 1000 }, (err, client) => {
                if (err) return reject(err);

                client.GetSecondaryNumbers(args, (err, result) => {
                    const response = result?.GetSecondaryNumbersResult;
                    try {
                        if (!response) {
                            return reject(new Error("SOAP response is empty or undefined."));
                        }
                        resolve(response);
                    } catch (parseErr) {
                        reject(new Error(`Failed to parse SOAP response: ${parseErr.message}`));
                    }
                });
            });
        });
    }

    /**
     * <summary>
     * Checks if a SOAP response is valid by verifying that it exists and either has no Error object
     * or the Error.TypeCode is not equal to '3'.
     * </summary>
     * @param {Object} response - The raw response object to validate.
     * @returns {boolean} True if the response is valid, false otherwise.
     */
    _isValid(response) {
        return response && (!response.Error || response.Error.TypeCode !== '3');
    }
}

export { GetSecondaryNumbersSoap };