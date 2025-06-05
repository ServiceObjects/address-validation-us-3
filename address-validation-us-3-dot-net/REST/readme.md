# AV3 - Address Validation US 3

DOTS Address Validation 3 US (“AV3”) is V3 version of our Address Validation and Address Validation 2 Web services. This service utilizes the latest .Net Framework, WCF, and can be used as a RESTful service or with SOAP. AV3 is designed to take an unstandardized address, validate it against the latest USPS data, and return standardized, deliverable addresses. The service provides corrected information such as the correct street location and zip plus four code, along with parsed address tokens, such as the PMB box number, pre- and post-directionals, county and state codes, and much more.

AV3 can provide instant address verification and correction to websites or enhancement to contact lists.  However, the output from AV3 must be considered carefully before the existence or non-existence of an address is decided.

# AV3 - GetBestMatches

GetBestMatches: Returns parsed and validated address elements including Delivery Point Validation (DPV), Residential Delivery Indicator (RDI), and Suite data. GetBestMatches will attempt to validate the input address against a CASS approved engine, and make corrections where possible. Multiple address candidates may be returned if a definitive decision cannot be made by the service.

## GetBestMatches Request URLs (Query String Parameters)
### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatches?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatches?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

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
//              BusinessName
//              Address2
//              City (when PostalCode is present)
//              State (when PostalCode is present)
//              PostalCode (when City and state are present)
//
// Though the API will run in any scenario, not adhering to these
// rules can result in error responses.

var gbmInput = new GetBestMatchesInput(
    BusinessName: "Acme Corp",
    Address:      "123 Main St",
    Address2:     "Suite 200",
    City:         "Los Angeles",
    State:        "CA",
    PostalCode:   "90012",
    LicenseKey:   "YOUR_LICENSE_KEY",
    IsLive:       true,    // production endpoints with fallback
    TimeoutSeconds: 15
);

// 2. Call the sync Invoke() method
GBMResponse gbmResult = GetBestMatchesClient.Invoke(gbmInput);

// 3. Inspect results
if (gbmResult.Error is null)
{
    foreach (var candidate in gbmResult.Addresses)
    {
        Console.WriteLine($"{candidate.ToString()}");
    }
}
else
{
    Console.WriteLine($"Error: {gbmResult.Error.ToString()}");
}
```

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best candidate with parsed and corrected address information. This operation may return multiple address candidates if a single best match cannot be determined.

## GetBestMatchesSingleLine Request URLs (Query String Parameters)
### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLine?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLine?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

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

var singleInput = new GetBestMatchesSingleLineInput(
    BusinessName: "Acme Corp",
    Address:      "123 Main St, Los Angeles, CA 90012",
    LicenseKey:   "YOUR_LICENSE_KEY",
    IsLive:       true,
    TimeoutSeconds: 15
);

AV3GbmResponse singleResult = GetBestMatchesSingleLineClient.Invoke(singleInput);

if (singleResult.Error is null)
{
    Console.WriteLine(singleResult.Addresses[0].ToString());
}
else
{
    Console.WriteLine($"Error: {singleResult.Error.Desc}");
}
```

# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly.  The inputs can be marginally incorrect, and this operation will correct them.  For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

## ValidateCityStateZip Request URLs (Query String Parameters)
### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/ValidateCityStateZip?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/ValidateCityStateZip?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

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

var cszInput = new ValidateCityStateZipInput(
    City:           "Los Angles",   // slightly misspelled
    State:          "Calif.",
    Zip:            "9001",         // missing the final digit
    LicenseKey:     "YOUR_LICENSE_KEY",
    IsLive:         true,
    TimeoutSeconds: 15
);

Av3CszResponse cszResult = ValidateCityStateZipClient.Invoke(cszInput);

if (cszResult.Error is null)
{
    Console.WriteLine(cszResult.ToString());
}
else
{
    Console.WriteLine($"Error: {cszResult.Error.ToString()}");
}
```

# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. The operation can be leveraged in conjunction with the GetBestMatches operation to find secondary numbers for input data that has either missing or incorrect unit information.

## GetSecondaryNumbers Request URLs (Query String Parameters)
### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetSecondaryNumbers?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetSecondaryNumbers?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

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

var secInput = new GetSecondaryNumbersInput(
    Address:       "123 Main St",
    City:          "Los Angeles",
    State:         "CA",
    PostalCode:    "90012",
    LicenseKey:    "YOUR_LICENSE_KEY",
    IsLive:        true,
    TimeoutSeconds: 15
);

Av3GsnResponse secResult = GetSecondaryNumbersClient.Invoke(secInput);

if (secResult.Error is null)
{
    Console.WriteLine($"Secondary numbers: {secResult.ToString()}");
}
else
{
    Console.WriteLine($"Error: {secResult.Error.ToString()}");
}
```
