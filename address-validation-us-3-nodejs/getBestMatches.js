'use strict';

/**
* @module AV3AddressValidation
* @description
*   Client module for the AV3 GetBestMatchesJson operation.
*   Validates and standardizes U.S. addresses via Service Objects’ AV3 API,
*   with built?in production/trial endpoints and automatic fallback logic.
*/

import fetch from 'node-fetch';

// Base endpoints for AV3 service
const ENDPOINTS = {
    production: 'https://ws.serviceobjects.com/av3/api.svc/',
    backup: 'https://wsbackup.serviceobjects.com/av3/api.svc/',
    trial: 'https://trial.serviceobjects.com/av3/api.svc/'
};

/**
 * Build a fully?qualified URL for the GetBestMatchesJson endpoint.
 * @param {object} params       Key/value pairs of query parameters.
 * @param {string} baseUrl      Base URL (must end with '/').
 * @returns {string}            Fully?qualified URL including encoded query string.
 */
function buildGetBestMatchesUrl(params, baseUrl) {
    const url = new URL('GetBestMatchesJson', baseUrl);
    Object.entries(params).forEach(([key, value]) => {
        url.searchParams.append(key, value ?? '');
    });
    return url.toString();
}

/**
 * Perform an HTTP GET request and parse JSON response.
 * @param {string} url          URL to request.
 * @returns {Promise<any>}      Parsed JSON body.
 * @throws {Error}              On HTTP or parse errors.
 */
async function httpGetJson(url) {
    const res = await fetch(url);
    if (!res.ok) throw new Error(`HTTP ${res.status} ${res.statusText}`);
    return res.json();
}

/**
 * @typedef {object} GetBestMatchesOptions
 * @property {string} businessName  Company name to assist suite parsing.
 * @property {string} address       Primary street address (e.g., '123 Main St').
 * @property {string} [address2]    Secondary address info (e.g., 'C/O John Smith').
 * @property {string} [city]        City; required if postalCode omitted.
 * @property {string} [state]       State code or full name; required if postalCode omitted.
 * @property {string} [postalCode]  5? or 9?digit ZIP; required if city/state omitted.
 * @property {string} licenseKey    Valid AV3 license key.
 * @property {'production'|'trial'} [environment='production']
 *                                 'production' for live+backup, 'trial' for trial only.
 * @property {number} [timeoutMs=15000]
 *                                 Request timeout in milliseconds.
 */

/**
 * Retrieve standardized address candidates via AV3 GetBestMatches.
 * Automatically falls back from production to backup on error TypeCode '3'.
 * @param {GetBestMatchesOptions} opts   Operation settings.
 * @returns {Promise<object>}            Service response JSON.
 * @throws {Error}                       On unrecoverable HTTP or service errors.
 */
export async function getBestMatches(opts) {
    const {
        businessName,
        address,
        address2 = '',
        city = '',
        state = '',
        postalCode = '',
        licenseKey,
        environment = 'production',
        timeoutMs = 15000
    } = opts;

    // Assemble query parameters matching API names
    const params = {
        BusinessName: businessName,
        Address: address,
        Address2: address2,
        City: city,
        State: state,
        PostalCode: postalCode,
        LicenseKey: licenseKey
    };

    // Choose primary URL based on environment
    const baseUrl = environment === 'trial' ? ENDPOINTS.trial : ENDPOINTS.production;
    const primaryUrl = buildGetBestMatchesUrl(params, baseUrl);

    // Helper to call with timeout
    const call = url => Promise.race([
        httpGetJson(url),
        new Promise((_, rej) => setTimeout(() => rej(new Error('Timeout')), timeoutMs))
    ]);

    try {
        let result = await call(primaryUrl);
        // Fallback if production + service-level fatal error
        if (environment === 'production' && result.Error?.TypeCode === '3') {
            const backupUrl = buildGetBestMatchesUrl(params, ENDPOINTS.backup);
            result = await call(backupUrl);
        }
        return result;
    } catch (err) {
        // On network or HTTP failure, retry backup for production
        if (environment === 'production') {
            const backupUrl = buildGetBestMatchesUrl(params, ENDPOINTS.backup);
            return call(backupUrl);
        }
        throw err;
    }
}