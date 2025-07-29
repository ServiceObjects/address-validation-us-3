![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AV3 - Address Validation US 3

DOTS Address Validation 3 US (“AV3”) is V3 version of our Address Validation and Address Validation 2 Web services. This service utilizes the latest .Net Framework, WCF, and can be used as a RESTful service or with SOAP. AV3 is designed to take an unstandardized address, validate it against the latest USPS data, and return standardized, deliverable addresses. The service provides corrected information such as the correct street location and zip plus four code, along with parsed address tokens, such as the PMB box number, pre- and post-directionals, county and state codes, and much more.

AV3 can provide instant address verification and correction to websites or enhancement to contact lists.  However, the output from AV3 must be considered carefully before the existence or non-existence of an address is decided.

## [Service Objects Website](https://serviceobjects.com)

# AV3 - GetBestMatches

Returns parsed and validated address elements including Delivery Point Validation (DPV), Residential Delivery Indicator (RDI), and Suite data. GetBestMatches will attempt to validate the input address against a CASS approved engine, and make corrections where possible. Multiple address addresss may be returned if a definitive decision cannot be made by the service.

### [GetBestMatches Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatches-recommended/)

## Library Usage

```
// 1. Build the input.
//
// Required fields:
//              Address
//              City (when PostalCode is missing)
//              State (when PostalCode is missing)
//              PostalCode (when City and state are missing)
//              LicenseKey
//              IsLive
//
// Optional fields:
//              BusinessName
//              Address2
//              City (when PostalCode is present)
//              State (when PostalCode is present)
//              PostalCode (when City and state are present)
//
// Though the API will run in any scenario, not adhering to these
// rules can result in error responses.

using address_validation_us_3_dot_net.REST;

var gbmInput = new GetBestMatchesClient.GetBestMatchesInput(
    BusinessName: "",
    Address:      "136 West Canon Perdido St",
    Address2:     "Suite D",
    City:         "Santa Barbara",
    State:        "CA",
    PostalCode:   "93101",
    LicenseKey:   "YOUR_LICENSE_KEY_HERE",
    IsLive:       true,    // production endpoints with fallback
    TimeoutSeconds: 15
);

// 2. Call the sync Invoke() method.
GBMResponse response = GetBestMatchesClient.Invoke(gbmInput);

// 3. Inspect results.
if (response.Error is null)
{
    Console.WriteLine("\r\n* Validation *\r\n");

    foreach (Address address in response.Addresses)
    {
        Console.WriteLine($"Address 1          : {address.Address1}");
        Console.WriteLine($"Address 2          : {address.Address2}");
        Console.WriteLine($"City               : {address.City}");
        Console.WriteLine($"State              : {address.State}");
        Console.WriteLine($"ZIP+4              : {address.Zip}");
        Console.WriteLine($"Is Residential     : {address.IsResidential}");
        Console.WriteLine($"DPV                : {address.DPV}");
        Console.WriteLine($"DPV Desc           : {address.DPVDesc}");
        Console.WriteLine($"DPV Notes          : {address.DPVNotes}");
        Console.WriteLine($"DPV Notes Desc     : {address.DPVNotesDesc}");
        Console.WriteLine($"Corrections        : {address.Corrections}");
        Console.WriteLine($"Corrections Desc   : {address.CorrectionsDesc}");
        Console.WriteLine($"Barcode Digits     : {address.BarcodeDigits}");
        Console.WriteLine($"Carrier Route      : {address.CarrierRoute}");
        Console.WriteLine($"Congress Code      : {address.CongressCode}");
        Console.WriteLine($"County Code        : {address.CountyCode}");
        Console.WriteLine($"County Name        : {address.CountyName}");
        Console.WriteLine($"Fragment House     : {address.FragmentHouse}");
        Console.WriteLine($"Fragment Pre Dir   : {address.FragmentPreDir}");
        Console.WriteLine($"Fragment Street    : {address.FragmentStreet}");
        Console.WriteLine($"Fragment Suffix    : {address.FragmentSuffix}");
        Console.WriteLine($"Fragment Post Dir  : {address.FragmentPostDir}");
        Console.WriteLine($"Fragment Unit      : {address.FragmentUnit}");
        Console.WriteLine($"Fragment           : {address.Fragment}");
        Console.WriteLine($"Fragment PMB Prefix: {address.FragmentPMBPrefix}");
        Console.WriteLine($"Fragment PMB Number: {address.FragmentPMBNumber}");
    }
    Console.WriteLine($"Is CASS: {response.IsCASS.ToString()}");
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best address with parsed and corrected address information. This operation may return multiple address addresss if a single best match cannot be determined.

### [GetBestMatchesSingleLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## Library Usage

```
//
// Required fields:
//              Address
//              LicenseKey
//              IsLive
//
// Optional fields:
//              BusinessName
//
// Though the API will run in any scenario, not adhering to these
// rules can result in error responses.

using address_validation_us_3_dot_net.REST;

var singleInput = new GetBestMatchesSingleLineClient.GetBestMatchesSingleLineInput(
    BusinessName: "",
    Address:      "136 West Canon Perdido St Suite D, Santa Barbara, CA 93101",
    LicenseKey:   "YOUR_LICENSE_KEY_HERE",
    IsLive:       true,
    TimeoutSeconds: 15
);

AV3GbmResponse response = GetBestMatchesSingleLineClient.Invoke(singleInput);

if (response.Error is null)
{
    Console.WriteLine("\r\n* Validation *\r\n");

    foreach (Address address in response.Addresses)
    {
        Console.WriteLine($"Address 1          : {address.Address1}");
        Console.WriteLine($"Address 2          : {address.Address2}");
        Console.WriteLine($"City               : {address.City}");
        Console.WriteLine($"State              : {address.State}");
        Console.WriteLine($"ZIP+4              : {address.Zip}");
        Console.WriteLine($"Is Residential     : {address.IsResidential}");
        Console.WriteLine($"DPV                : {address.DPV}");
        Console.WriteLine($"DPV Desc           : {address.DPVDesc}");
        Console.WriteLine($"DPV Notes          : {address.DPVNotes}");
        Console.WriteLine($"DPV Notes Desc     : {address.DPVNotesDesc}");
        Console.WriteLine($"Corrections        : {address.Corrections}");
        Console.WriteLine($"Corrections Desc   : {address.CorrectionsDesc}");
        Console.WriteLine($"Barcode Digits     : {address.BarcodeDigits}");
        Console.WriteLine($"Carrier Route      : {address.CarrierRoute}");
        Console.WriteLine($"Congress Code      : {address.CongressCode}");
        Console.WriteLine($"County Code        : {address.CountyCode}");
        Console.WriteLine($"County Name        : {address.CountyName}");
        Console.WriteLine($"Fragment House     : {address.FragmentHouse}");
        Console.WriteLine($"Fragment Pre Dir   : {address.FragmentPreDir}");
        Console.WriteLine($"Fragment Street    : {address.FragmentStreet}");
        Console.WriteLine($"Fragment Suffix    : {address.FragmentSuffix}");
        Console.WriteLine($"Fragment Post Dir  : {address.FragmentPostDir}");
        Console.WriteLine($"Fragment Unit      : {address.FragmentUnit}");
        Console.WriteLine($"Fragment           : {address.Fragment}");
        Console.WriteLine($"Fragment PMB Prefix: {address.FragmentPMBPrefix}");
        Console.WriteLine($"Fragment PMB Number: {address.FragmentPMBNumber}");
    }
    Console.WriteLine($"Is CASS: {response.IsCASS.ToString()}");
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```

# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly.  The inputs can be marginally incorrect, and this operation will correct them.  For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

### [ValidateCityStateZip Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-validatecitystatezip/)

## Library Usage

```
// 1. Build the input
//
// Required fields:
//              City (when PostalCode is missing)
//              State (when PostalCode is missing)
//              PostalCode (when City and state are missing)
//              LicenseKey
//              IsLive
//
// Though the API will run in any scenario, not adhering to these
// rules can result in error responses.

using address_validation_us_3_dot_net.REST;

var cszInput = new ValidateCityStateZipClient.ValidateCityStateZipInput(
    City:           "Santa Barbara",
    State:          "CA",
    Zip:            "93101",
    LicenseKey:     "YOUR_LICENSE_KEY_HERE",
    IsLive:         true,
    TimeoutSeconds: 15
);

Av3CszResponse response = ValidateCityStateZipClient.Invoke(cszInput);

if (response.Error is null)
{
    Console.WriteLine("\r\n* Validation *\r\n");

    Console.WriteLine($"City                          : {response.CityStateZip.City}");
    Console.WriteLine($"State                         : {response.CityStateZip.State}");
    Console.WriteLine($"ZIP Code                      : {response.CityStateZip.Zip}");
    Console.WriteLine($"General Delivery Service      : {response.CityStateZip.GeneralDeliveryService}");
    Console.WriteLine($"Street Service                : {response.CityStateZip.StreetService}");
    Console.WriteLine($"PO Box Service                : {response.CityStateZip.POBoxService}");
    Console.WriteLine($"PO Box Range Low              : {response.CityStateZip.POBoxRangeLow}");
    Console.WriteLine($"PO Box Range High             : {response.CityStateZip.POBoxRangeHigh}");
    Console.WriteLine($"Rural Route / Highway Contract: {response.CityStateZip.RRHCService}");
    Console.WriteLine($"Urbanization Service          : {response.CityStateZip.UrbanizationService}");
    Console.WriteLine($"Is Unique ZIP Code            : {response.CityStateZip.IsUniqueZipCode}");
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```

# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. The operation can be leveraged in conjunction with the GetBestMatches operation to find secondary numbers for input data that has either missing or incorrect unit information.

### [GetSecondaryNumbers Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getsecondarynumbers/)

## Library Usage

```
// 1. Build the input
//
// Required fields:
//              Address
//              City (when PostalCode is missing)
//              State (when PostalCode is missing)
//              PostalCode (when City and state are missing)
//              LicenseKey
//              IsLive
//
// Optional fields:
//              City (when PostalCode is present)
//              State (when PostalCode is present)
//              PostalCode (when City and state are present)
//
// Though the API will run in any scenario, not adhering to these
// rules can result in error responses.

using address_validation_us_3_dot_net.REST;

var secInput = new GetSecondaryNumbersClient.GetSecondaryNumbersInput(
    Address:       "136 West Canon Perdido St",
    City:          "Santa Barbara",
    State:         "CA",
    PostalCode:    "93101",
    LicenseKey:    "YOUR_LICENSE_KEY_HERE",
    IsLive:        true,
    TimeoutSeconds: 15
);

Av3GsnResponse response = GetSecondaryNumbersClient.Invoke(secInput);

if (response.Error is null)
{
    Console.WriteLine("\r\n* Validation *\r\n");

    Console.WriteLine($"Address          : {response.Address1}");
    Console.WriteLine($"City             : {response.City}");
    Console.WriteLine($"State            : {response.State}");
    Console.WriteLine($"ZIP+4            : {response.Zip}");
    Console.WriteLine($"Total Unit Count : {response.TotalCount.ToString()}");
    Console.WriteLine($"Secondary Numbers:");


    foreach (string unit in response.SecondaryNumbers)
    {
        Console.WriteLine($"                  {unit}");
    }
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```
