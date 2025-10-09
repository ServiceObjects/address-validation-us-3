import { GetBestMatchesClient } from '../address-validation-us-3-nodejs/REST/get_best_matches_rest.js';

async function GetBestMatchesRestGo(licenseKey, isLive) {
    console.log("\n-----------------------------------------------------");
    console.log("Address Validation - US 3 - GetBestMatches - REST SDK");
    console.log("-----------------------------------------------------");

    const businessName = "Service Objects";
    const address = "136 W Canon Perdido St";
    const address2 = "Ste D";
    const city = "Santa Barbara";
    const state = "CA";
    const postalCode = "93101";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`BusinessName   : ${businessName}`);
    console.log(`Address        : ${address}`);
    console.log(`Address2       : ${address2}`);
    console.log(`City           : ${city}`);
    console.log(`State          : ${state}`);
    console.log(`PostalCode     : ${postalCode}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const response = await GetBestMatchesClient.invoke(
            businessName,
            address,
            address2,
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
            console.log(`IsCASS: ${response.IsCASS || false}`);
        } else {
            console.log("No address info found.");
        }

        console.log("\n* Addresses *\n");
        if (response && response.Addresses && response.Addresses.length > 0) {
            response.Addresses.forEach((address) => {
                console.log(`Address1         : ${address.Address1}`);
                console.log(`Address2         : ${address.Address2}`);
                console.log(`City             : ${address.City}`);
                console.log(`State            : ${address.State}`);
                console.log(`Zip              : ${address.Zip}`);
                console.log(`IsResidential    : ${address.IsResidential}`);
                console.log(`DPV              : ${address.DPV}`);
                console.log(`DPVDesc          : ${address.DPVDesc}`);
                console.log(`DPVNotes         : ${address.DPVNotes}`);
                console.log(`DPVNotesDesc     : ${address.DPVNotesDesc}`);
                console.log(`Corrections      : ${address.Corrections}`);
                console.log(`CorrectionsDesc  : ${address.CorrectionsDesc}`);
                console.log(`BarcodeDigits    : ${address.BarcodeDigits}`);
                console.log(`CarrierRoute     : ${address.CarrierRoute}`);
                console.log(`CongressCode     : ${address.CongressCode}`);
                console.log(`CountyCode       : ${address.CountyCode}`);
                console.log(`CountyName       : ${address.CountyName}`);
                console.log(`FragmentHouse    : ${address.FragmentHouse}`);
                console.log(`FragmentPreDir   : ${address.FragmentPreDir}`);
                console.log(`FragmentStreet   : ${address.FragmentStreet}`);
                console.log(`FragmentSuffix   : ${address.FragmentSuffix}`);
                console.log(`FragmentPostDir  : ${address.FragmentPostDir}`);
                console.log(`FragmentUnit     : ${address.FragmentUnit}`);
                console.log(`Fragment         : ${address.Fragment}`);
                console.log(`FragmentPMBPrefix: ${address.FragmentPMBPrefix}`);
                console.log(`FragmentPMBNumber: ${address.FragmentPMBNumber}`);
            });
        } else {
            console.log("No addresses found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetBestMatchesRestGo };