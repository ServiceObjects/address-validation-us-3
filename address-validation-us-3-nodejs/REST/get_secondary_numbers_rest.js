import axios from 'axios';
import querystring from 'querystring';
import { GetSecondaryNumbersResponse } from './av3_response.js';

/**
 * @constant
 * @type {string}
 * @description The base URL for the live ServiceObjects Address Validation (AV3) API service.
 */
const LiveBaseUrl = 'https://sws.serviceobjects.com/av3/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the backup ServiceObjects Address Validation (AV3) API service.
 */
const BackupBaseUrl = 'https://swsbackup.serviceobjects.com/av3/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the trial ServiceObjects Address Validation (AV3) API service.
 */
const TrialBaseUrl = 'https://trial.serviceobjects.com/av3/api.svc/json/';

/**
 * <summary>
 * Checks if a response from the API is valid by verifying that it either has no Error object
 * or the Error.TypeCode is not equal to '3'.
 * </summary>
 * <param name="response" type="Object">The API response object to validate.</param>
 * <returns type="boolean">True if the response is valid, false otherwise.</returns>
 */
const isValid = (response) => !response?.Error || response.Error.TypeCode !== '3';

/**
 * <summary>
 * Constructs a full URL for the GetSecondaryNumbers API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}GetSecondaryNumbersJson?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<GetSecondaryNumbersResponse>">A promise that resolves to a GetSecondaryNumbersResponse object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new GetSecondaryNumbersResponse(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Validation (AV3) API's GetSecondaryNumbers endpoint,
 * retrieving secondary address information (e.g., apartment or suite numbers) for a given U.S. address with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const GetSecondaryNumbersClient = {
    /**
     * <summary>
     * Asynchronously invokes the GetSecondaryNumbers API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.TypeCode == '3') in live mode.
     * </summary>
     * @param {string} Address - Address line 1 of the address (e.g., "123 Main Street"). Required.
     * @param {string} City - The city of the address (e.g., "New York"). Required.
     * @param {string} State - The state of the address (e.g., "NY"). Required.
     * @param {string} PostalCode - The zip code of the address. Required.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {Promise<GetSecondaryNumbersResponse>} - A promise that resolves to a GetSecondaryNumbersResponse object.
     */
    async invokeAsync(Address, City, State, PostalCode, LicenseKey, isLive = true, timeoutSeconds = 15) {
        const params = {
            Address,
            City,
            State,
            PostalCode,
            LicenseKey
        };

        // Remove null/undefined params to avoid empty query params
        Object.keys(params).forEach(key => params[key] == null && delete params[key]);

        const url = buildUrl(params, isLive ? LiveBaseUrl : TrialBaseUrl);
        let response = await httpGet(url, timeoutSeconds);

        if (isLive && !isValid(response)) {
            const fallbackUrl = buildUrl(params, BackupBaseUrl);
            const fallbackResponse = await httpGet(fallbackUrl, timeoutSeconds);
            return fallbackResponse;
        }
        return response;
    },

    /**
     * <summary>
     * Synchronously invokes the GetSecondaryNumbers API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {string} Address - Address line 1 of the address (e.g., "123 Main Street"). Required.
     * @param {string} City - The city of the address (e.g., "New York"). Required.
     * @param {string} State - The state of the address (e.g., "NY"). Required.
     * @param {string} PostalCode - The zip code of the address. Required.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {GetSecondaryNumbersResponse} - A GetSecondaryNumbersResponse object with secondary address details or an error.
     */
    invoke(Address, City, State, PostalCode, LicenseKey, isLive = true, timeoutSeconds = 15) {
        return (async () => await this.invokeAsync(
            Address, City, State, PostalCode, LicenseKey, isLive, timeoutSeconds
        ))();
    }
};

export { GetSecondaryNumbersClient, GetSecondaryNumbersResponse };