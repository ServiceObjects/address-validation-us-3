![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AV3 - Address Validation – US 3

DOTS Address Validation 3 US (“AV3”) is a new version of our Address Validation and Address Validation 2 Web services. This service utilizes the latest .Net Framework, WCF, and can be used as a RESTful service or with SOAP. AV3 is designed to take an unstandardized address, validate it against the latest USPS data, and return standardized, deliverable addresses. 

The service provides corrected information such as the correct street location and zip plus four code, along with parsed address tokens, such as the PMB box number, pre- and post-directionals, county and state codes, and much more.

AV3 can provide instant address verification and correction to websites or enhancement to contact lists.  However, the output from AV3 must be considered carefully before the existence or non-existence of an address is decided.

## [Service Objects Website](https://serviceobjects.com)

# AV3 - GetBestMatches

Returns parsed and validated address elements including Delivery Point Validation (DPV), Residential Delivery Indicator (RDI), and Suite data. GetBestMatches will attempt to validate the input address against a CASS approved engine, and make corrections where possible.

Multiple address candidates may be returned if a definitive decision cannot be made by the service.

### [GetBestMatches Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatches-recommended/)

## Library Usage

```
// 1. Build the input
//
//  Fields:
//        businessName
//        address
//        address2
//        city
//        state
//        postalCode
//        timeoutSeconds
//        licenseKey
//        isLive

import { GetBestMatchesSoap } from '../address-validation-us-3-nodejs/SOAP/get_best_matches_soap.js';

const businessName = "Service Objects";
const address = "136 W Canon Perdido St";
const address2 = "Ste D";
const city = "Santa Barbara";
const state = "CA";
const postalCode = "93101";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync Invoke() method.
const av3 = new GetBestMatchesSoap(
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
const response = await av3.invokeAsync();

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
}

console.log("\n* Address Info *\n");
if (response && response.Addresses && response.Addresses.Address) 
{
        console.log(`IsCASS           : ${response.IsCASS}`);
    response.Addresses.Address.forEach((address, index) => {
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
} 
else
{
    console.log("No address candidates found.");
}
```
# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. 

The operation can be used with GetBestMatches to find secondary numbers for addresses missing or containing incorrect unit information.

### [GetSecondaryNumbers Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getsecondarynumbers/)

## Library Usage

```
// 1. Build the input
//
//  Fields:
//        address
//        city
//        state
//        postalCode
//        timeoutSeconds
//        licenseKey
//        isLive

import { GetSecondaryNumbersSoap } from '..//address-validation-us-3-nodejs//SOAP/get_secondary_numbers_soap.js';

const address = "136 W Canon Perdido St";
const city = "Santa Barbara";
const state = "CA";
const postalCode = "93101";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync Invoke() method.
const av3 = new GetSecondaryNumbersSoap(
    address,
    city,
    state,
    postalCode,
    licenseKey,
    isLive,
    timeoutSeconds
);
const response = await av3.invokeAsync();

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
}

console.log("\n* Address Info *\n");
if (response && response.Address1) 
{
    console.log(`Address1  : ${response.Address1}`);
    console.log(`City      : ${response.City}`);
    console.log(`State     : ${response.State}`);
    console.log(`Zip       : ${response.Zip}`);
    console.log(`TotalCount: ${response.TotalCount}`);
            
    console.log("\n* Secondary Numbers *\n");
    if (response.SecondaryNumbers?.string && response.SecondaryNumbers.string.length > 0) 
    {
        response.SecondaryNumbers.string.forEach((sec, index) => {
            console.log(`Secondary Number: ${sec}`);
        });
    } 
    else
    {
        console.log("No secondary numbers found.");
    }
}
else
{
    console.log("No address info found.");
}
```
# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly. 

The inputs can be marginally incorrect, and this operation will correct them. For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

### [ValidateCityStateZip Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-validatecitystatezip/)

## Library Usage

```
// 1. Build the input
//
//  Fields:
//        city
//        state
//        postalCode
//        timeoutSeconds
//        licenseKey
//        isLive

import { ValidateCityStateZipSoap } from '../address-validation-us-3-nodejs//SOAP/validate_city_state_zip_soap.js';

const city = "Santa Barbara";
const state = "CA";
const zip = "93101";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";


// 2. Call the sync Invoke() method.
const av3 = new ValidateCityStateZipSoap(
    city,
    state,
    zip,
    licenseKey,
    isLive,
    timeoutSeconds
);
const response = await av3.invokeAsync();

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
}

console.log("\n* City State Zip Info *\n");
if (response && response.CityStateZip)
{
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
}
else 
{
    console.log("No city/state/zip info found.");
}
```
# AV3 - GetBestMatchesSingeLine

Takes a single line of address information as the input and returns the best candidate with parsed and corrected address information. This operation may return multiple address candidates if a single best match cannot be determined.

### [GetBestMatchesSingeLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## Library Usage

```
// 1. Build the input
//
//  Fields:
//        businessName
//        address
//        timeoutSeconds
//        licenseKey
//        isLive

import { GetBestMatchesSingleLineSoap } from '../address-validation-us-3-nodejs/SOAP/get_best_matches_single_line_soap.js';

const businessName = "Service Objects";
const address = "136 W Canon Perdido St, Suite D, Santa Barbara, CA 93101";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";


// 2. Call the sync Invoke() method.
const av3 = new GetBestMatchesSingleLineSoap(
    businessName,
    address,
    licenseKey,
    isLive,
    timeoutSeconds
);
const response = await av3.invokeAsync();

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
}

console.log("\n* Address Info *\n");
if (response && response.Addresses && response.Addresses.Address) 
{
        console.log(`IsCASS           : ${response.IsCASS}`);
    response.Addresses.Address.forEach((address) => {
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
} 
else 
{
    console.log("No address candidates found.");
}
```

