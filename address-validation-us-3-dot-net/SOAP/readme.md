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
// (1) Instantiate the service wrapper
var av3 = new BestMatchesValidation();

// (2) Provide your input data
string businessName = "";
string address1     = "136 West Canon Perdido St";
string address2     = "Suite D";            // optional
string city         = "Santa Barbara";
string state        = "CA";
string zip          = "93101";
string licenseKey   = "YOUR_LICENSE_KEY_HERE";


// (3) Call the service
var response = av3.GetBestMatches(
    businessName,
    address1,
    address2,
    city,
    state,
    zip,
    licenseKey
);

// (4) Print to console (or process however you like)
if (response.Error != null)
{
	Console.WriteLine("GetBestMatches Error:");
	Console.WriteLine($"  Type    : {response.Error.Type}");
	Console.WriteLine($"  TypeCode: {response.Error.TypeCode}");
	Console.WriteLine($"  Desc    : {response.Error.Desc}");
	Console.WriteLine($"  DescCode: {response.Error.DescCode}");
}
else
{
	int matchCount = 1;
    foreach (var addr in response.Addresses)
    {
        Console.WriteLine($"  --- Match #{matchCount++} ---");
        Console.WriteLine($"   Address1      : {addr.Address1}");
        Console.WriteLine($"   Address2      : {addr.Address2}");
        Console.WriteLine($"   City           : {addr.City}");
        Console.WriteLine($"   State          : {addr.State}");
        Console.WriteLine($"   Zip            : {addr.Zip}");
        Console.WriteLine($"   IsResidential : {addr.is}");
        Console.WriteLine($"   DPV            : {addr.DPV} ({addr.DPVDesc})");
        Console.WriteLine($"   DPVNotes      : {addr.DPVNotes} ({addr.DPVNotesDesc})");
        Console.WriteLine($"   Corrections    : {addr.Corrections} ({addr.CorrectionsDesc})");
        Console.WriteLine($"   BarcodeDigits : {addr.BarcodeDigits}");
        Console.WriteLine($"   CarrierRoute   : {addr.CarrierRoute}");
        Console.WriteLine($"   CongressCode  : {addr.CongressCode}");
        Console.WriteLine($"   CountyCode   : {addr.CountyCode}");
        Console.WriteLine($"   CountyName    : {addr.CountyName}");
        Console.WriteLine($"   FragmentHouse : {addr.FragmentHouse}");
        Console.WriteLine($"   FragmentPreDir : {addr.FragmentPreDir}");
        Console.WriteLine($"   FragmentStreet : {addr.FragmentStreet}");
        Console.WriteLine($"   FragmentSuffix : {addr.FragmentSuffix}");
        Console.WriteLine($"   FragmentPostDir: {addr.FragmentPostDir}");
        Console.WriteLine($"   FragmentUnit  : {addr.FragmentUnit}");
        Console.WriteLine($"   Fragment       : {addr.Fragment}");
        Console.WriteLine($"   FragmentPMBPrefix: {addr.FragmentPMBPrefix}");
        Console.WriteLine($"   FragmentPMBNumber: {addr.FragmentPMBNumber}");
    }
}
```

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best address with parsed and corrected address information. This operation may return multiple address addresss if a single best match cannot be determined.

### [GetBestMatchesSingleLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## Library Usage

```
// (1) Instantiate the service wrapper
var av3 = new GetBestMatchesSingleLineValidation();

// (2) Provide your input data
string businessName = "";
string fullAddress  = "136 West Canon Perdido St Suite D, Santa Barbara, CA 93101";
string licenseKey   = "YOUR_LICENSE_KEY_HERE";

// (3) Call GetBestMatchesSingleLine
var response = av3.GetBestMatchesSingleLine(
    businessName,
    fullAddress,
    licenseKey
);

// (4) Print to console (or process however you like)
if (response.Error != null)
{
	Console.WriteLine("GetBestMatchesSingleLine Error:");
	Console.WriteLine($"  Type    : {response.Error.Type}");
	Console.WriteLine($"  TypeCode: {response.Error.TypeCode}");
	Console.WriteLine($"  Desc    : {response.Error.Desc}");
	Console.WriteLine($"  DescCode: {response.Error.DescCode}");
}
else
{
	int matchCount = 1;
    foreach (var addr in response.Addresses)
    {
        Console.WriteLine($"  --- Match #{matchCount++} ---");
        Console.WriteLine($"   Address1      : {addr.Address1}");
        Console.WriteLine($"   Address2      : {addr.Address2}");
        Console.WriteLine($"   City           : {addr.City}");
        Console.WriteLine($"   State          : {addr.State}");
        Console.WriteLine($"   Zip            : {addr.Zip}");
        Console.WriteLine($"   IsResidential : {addr.is}");
        Console.WriteLine($"   DPV            : {addr.DPV} ({addr.DPVDesc})");
        Console.WriteLine($"   DPVNotes      : {addr.DPVNotes} ({addr.DPVNotesDesc})");
        Console.WriteLine($"   Corrections    : {addr.Corrections} ({addr.CorrectionsDesc})");
        Console.WriteLine($"   BarcodeDigits : {addr.BarcodeDigits}");
        Console.WriteLine($"   CarrierRoute   : {addr.CarrierRoute}");
        Console.WriteLine($"   CongressCode  : {addr.CongressCode}");
        Console.WriteLine($"   CountyCode   : {addr.CountyCode}");
        Console.WriteLine($"   CountyName    : {addr.CountyName}");
        Console.WriteLine($"   FragmentHouse : {addr.FragmentHouse}");
        Console.WriteLine($"   FragmentPreDir : {addr.FragmentPreDir}");
        Console.WriteLine($"   FragmentStreet : {addr.FragmentStreet}");
        Console.WriteLine($"   FragmentSuffix : {addr.FragmentSuffix}");
        Console.WriteLine($"   FragmentPostDir: {addr.FragmentPostDir}");
        Console.WriteLine($"   FragmentUnit  : {addr.FragmentUnit}");
        Console.WriteLine($"   Fragment       : {addr.Fragment}");
        Console.WriteLine($"   FragmentPMBPrefix: {addr.FragmentPMBPrefix}");
        Console.WriteLine($"   FragmentPMBNumber: {addr.FragmentPMBNumber}");
    }
}
```

# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly.  The inputs can be marginally incorrect, and this operation will correct them.  For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

### [ValidateCityStateZip Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-validatecitystatezip/)

## Library Usage

```
// (1) Instantiate the service wrapper
var av3 = new CityStateZipValidation();

// (2) Provide your input data
string city       = "Santa Barbara";
string state      = "CA";
string zip        = "93101";
string licenseKey = "YOUR_LICENSE_KEY_HERE";

// (3) Call ValidateCityStateZip
var response = av3.ValidateCityStateZip(
    city,
    state,
    zip,
    licenseKey
);

// (4) Check for errors
if (response.Error != null)
{
    Console.WriteLine("ValidateCityStateZip Error:");
    Console.WriteLine($"  Type    : {response.Error.Type}");
    Console.WriteLine($"  TypeCode: {response.Error.TypeCode}");
    Console.WriteLine($"  Desc    : {response.Error.Desc}");
    Console.WriteLine($"  DescCode: {response.Error.DescCode}");
}
else
{
    // (5) Print the validated/corrected city, state, and zip
    Console.WriteLine("ValidateCityStateZip Result:");
    Console.WriteLine($"  City : {response.City}");
    Console.WriteLine($"  State: {response.State}");
    Console.WriteLine($"  Zip  : {response.Zip}");
    Console.WriteLine($"  Zip  : {response.GeneralDeliveryService}");
    Console.WriteLine($"  Zip  : {response.POBoxService}");
    Console.WriteLine($"  Zip  : {response.StreetService}");
    Console.WriteLine($"  Zip  : {response.RRHCService}");
    Console.WriteLine($"  Zip  : {response.UrbanizationService}");
    Console.WriteLine($"  Zip  : {response.POBoxRangeLow}");
    Console.WriteLine($"  Zip  : {response.POBoxRangeHigh}");
    Console.WriteLine($"  Zip  : {response.IsUniqueZipCode}");
}
```

# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. The operation can be leveraged in conjunction with the GetBestMatches/GetBestMatchesSingleLine operation to find secondary numbers for input data that has either missing or incorrect unit information.

### [GetSecondaryNumbers Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getsecondarynumbers/)

## Library Usage

```
// (1) Instantiate the service wrapper
var av3 = new GetSecondaryNumbersValidation();

// (2) Provide your input data
string address    = "136 West Canon Perdido St";
string city       = "Santa Barbara";
string state      = "CA";
string zip        = "93101";
string licenseKey = "YOUR_LICENSE_KEY_HERE";

// (3) Call GetSecondaryNumbers
var response = av3.GetSecondaryNumbers(
    address,
    city,
    state,
    zip,
    licenseKey
);

// (4) Check for errors
if (response.Error != null)
{
	Console.WriteLine("GetSecondaryNumbers Error:");
	Console.WriteLine($"  Type    : {response.Error.Type}");
	Console.WriteLine($"  TypeCode: {response.Error.TypeCode}");
	Console.WriteLine($"  Desc    : {response.Error.Desc}");
	Console.WriteLine($"  DescCode: {response.Error.DescCode}");
}
else
{
    Console.WriteLine("Secondary Number Results:");
    Console.WriteLine($"  Carrier Route: {response.Address1}");
    Console.WriteLine($"  Carrier Route: {response.City}");
    Console.WriteLine($"  Carrier Route: {response.State}");
    Console.WriteLine($"  Carrier Route: {response.Zip}");
    Console.WriteLine($"  Carrier Route: {response.TotalCount}");
    Console.WriteLine("Secondary Numbers:");
    if (response.SecondaryNumbers != null)
    {
	    foreach (var num in response.SecondaryNumbers)
	    {
		    Console.WriteLine($"  Carrier Route: {num.CarrierRoute}");
		    Console.WriteLine($"  Secondary Number: {num.SecondaryNumber}");
	    }
    }
}
```
