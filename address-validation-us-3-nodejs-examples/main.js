import {GetBestMatchesRestGo} from './get_best_matches_rest_sdk_example.js';
import {GetBestMatchesSoapGo} from './get_best_matches_soap_sdk_example.js';
import {GetBestMatchesSingleLineRestGo} from './get_best_matches_single_line_rest_sdk_example.js';
import {GetBestMatchesSingleLineSoapGo} from './get_best_matches_single_line_soap_sdk_example.js';
import {GetSecondaryNumbersRestGo} from './get_secondary_numbers_rest_sdk_example.js';
import {GetSecondaryNumbersSoapGo} from './get_secondary_numbers_soap_sdk_example.js';
import {ValidateCityStateZipRestGo} from './validate_city_state_zip_rest.js';
import {ValidateCityStateZipSoapGo} from './validate_city_state_zip_soap.js';

export async function main()
{
    //Your license key from Service Objects.
    //Trial license keys will only work on the
    //trial environments and production license
    //keys will only work on production environments.
    const licenseKey = "YOUR LICENSE KEY";
    const isLive = true;

    // Address Validation - US 3 - GetBestMatches - REST SDK
    await GetBestMatchesRestGo(licenseKey, isLive);

    // Address Validation - US 3 - GetBestMatches - SOAP SDK
    await GetBestMatchesSoapGo(licenseKey, isLive);

    // Address Validation - US 3 - GetBestMatchesSingleLine - REST SDK
    await GetBestMatchesSingleLineRestGo(licenseKey, isLive);

    // Address Validation - US 3 - GetBestMatchesSingleLine - SOAP SDK
    await GetBestMatchesSingleLineSoapGo(licenseKey, isLive);

    // Address Validation - US 3 - GetSecondaryNumbers - REST SDK
    await GetSecondaryNumbersRestGo(licenseKey, isLive);

    // Address Validation - US 3 - GetSecondaryNumbers - SOAP SDK
    await GetSecondaryNumbersSoapGo(licenseKey, isLive);

    // Address Validation - US 3 - ValidateCityStateZip - REST SDK
    await ValidateCityStateZipRestGo(licenseKey, isLive);

    // Address Validation - US 3 - ValidateCityStateZip - SOAP SDK
    await ValidateCityStateZipSoapGo(licenseKey, isLive);
   
}
main().catch((error) => {
  console.error("An error occurred:", error);
  process.exit(1);
});