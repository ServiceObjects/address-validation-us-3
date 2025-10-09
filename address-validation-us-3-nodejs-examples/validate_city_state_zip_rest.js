import { ValidateCityStateZipClient } from '../address-validation-us-3-nodejs/REST/validate_city_state_zip_rest.js';

async function ValidateCityStateZipRestGo(licenseKey, isLive) {
    console.log("\n-----------------------------------------------------------");
    console.log("Address Validation - US 3 - ValidateCityStateZip - REST SDK");
    console.log("-----------------------------------------------------------");

    const city = "Santa Barbara";
    const state = "CA";
    const zip = "93101";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`City           : ${city}`);
    console.log(`State          : ${state}`);
    console.log(`Zip            : ${zip}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const response = await ValidateCityStateZipClient.invoke(
            city,
            state,
            zip,
            licenseKey,
            isLive,
            timeoutSeconds
        );

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Type    : ${response.Error.Type}`);
            console.log(`Error TypeCode: ${response.Error.TypeCode}`);
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error DescCode: ${response.Error.DescCode}`);
            return;
        }

        console.log("\n* City State Zip Info *\n");
        if (response && response.CityStateZip) {
            console.log(`City                  : ${response.CityStateZip.City}`);
            console.log(`State                 : ${response.CityStateZip.State}`);
            console.log(`Zip                   : ${response.CityStateZip.Zip}`);
            console.log(`GeneralDeliveryService: ${response.CityStateZip.GeneralDeliveryService}`);
            console.log(`POBoxService          : ${response.CityStateZip.POBoxService}`);
            console.log(`StreetService         : ${response.CityStateZip.StreetService}`);
            console.log(`RRHCService           : ${response.CityStateZip.RRHCService}`);
            console.log(`UrbanizationService   : ${response.CityStateZip.UrbanizationService}`);
            console.log(`POBoxRangeLow         : ${response.CityStateZip.POBoxRangeLow}`);
            console.log(`POBoxRangeHigh        : ${response.CityStateZip.POBoxRangeHigh}`);
            console.log(`IsUniqueZipCode       : ${response.CityStateZip.IsUniqueZipCode}`);
        } else {
            console.log("No city/state/zip info found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { ValidateCityStateZipRestGo };