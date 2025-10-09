import { GetSecondaryNumbersClient } from '../address-validation-us-3-nodejs/REST/get_secondary_numbers_rest.js';

async function GetSecondaryNumbersRestGo(licenseKey, isLive) {
    console.log("\n----------------------------------------------------------");
    console.log("Address Validation - US 3 - GetSecondaryNumbers - REST SDK");
    console.log("----------------------------------------------------------");

    const address = "136 W Canon Perdido St";
    const city = "Santa Barbara";
    const state = "CA";
    const postalCode = "93101";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Address        : ${address}`);
    console.log(`City           : ${city}`);
    console.log(`State          : ${state}`);
    console.log(`PostalCode     : ${postalCode}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const response = await GetSecondaryNumbersClient.invoke(
            address,
            city,
            state,
            postalCode,
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

        console.log("\n* Address Info *\n");
        if (response) {
            console.log(`Address1  : ${response.Address1}`);
            console.log(`City      : ${response.City}`);
            console.log(`State     : ${response.State}`);
            console.log(`Zip       : ${response.Zip}`);
            console.log(`TotalCount: ${response.TotalCount}`);
            
            console.log("\n* Secondary Numbers *\n");
            if (response.SecondaryNumbers && response.SecondaryNumbers.length > 0) {
                response.SecondaryNumbers.forEach((sec, index) => {
                    console.log(`Secondary Number: ${sec}`);
                });
            } else {
                console.log("No secondary numbers found.");
            }
        } else {
            console.log("No address info found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetSecondaryNumbersRestGo };