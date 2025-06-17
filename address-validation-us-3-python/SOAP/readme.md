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
# 1. Build the input.
#
# Required fields:
#              Address
#              City (when PostalCode is missing)
#              State (when PostalCode is missing)
#              PostalCode (when City and state are missing)
#              LicenseKey
#              IsLive
#
# Optional fields:
#              BusinessName
#              Address2
#              City (when PostalCode is present)
#              State (when PostalCode is present)
#              PostalCode (when City and state are present)
#
# Though the API will run in any scenario, not adhering to these
# rules can result in error responses.

from get_best_matches_soap import GetBestMatchesValidation

# 2. Call the validation method.
service = GetBestMatchesValidation(license, is_live=True, timeout_ms=10000)

response = service.get_best_matches(
            business_name="",
            address1="136 West Canon Perdido St",
            address2="Suite D",
            city="Santa Barbara",
            state="CA",
            postal_code="93101"
        )

# 3. Inspect results.
if not hasattr(response, 'Error'):
    print("\n* Validation *\n")
    for address in response.Addresses[0]:
        print(f"Address 1          : {address.Address1}")
        print(f"Address 2          : {address.Address2}")
        print(f"City               : {address.City}")
        print(f"State              : {address.State}")
        print(f"ZIP+4              : {address.Zip}")
        print(f"Is Residential     : {address.IsResidential}")
        print(f"DPV                : {address.DPV}")
        print(f"DPV Desc           : {address.DPVDesc}")
        print(f"DPV Notes          : {address.DPVNotes}")
        print(f"DPV Notes Desc     : {address.DPVNotesDesc}")
        print(f"Corrections        : {address.Corrections}")
        print(f"Corrections Desc   : {address.CorrectionsDesc}")
        print(f"Barcode Digits     : {address.BarcodeDigits}")
        print(f"Carrier Route      : {address.CarrierRoute}")
        print(f"Congress Code      : {address.CongressCode}")
        print(f"County Code        : {address.CountyCode}")
        print(f"County Name        : {address.CountyName}")
        print(f"Fragment House     : {address.FragmentHouse}")
        print(f"Fragment Pre Dir   : {address.FragmentPreDir}")
        print(f"Fragment Street    : {address.FragmentStreet}")
        print(f"Fragment Suffix    : {address.FragmentSuffix}")
        print(f"Fragment Post Dir  : {address.FragmentPostDir}")
        print(f"Fragment Unit      : {address.FragmentUnit}")
        print(f"Fragment           : {address.Fragment}")
        print(f"Fragment PMB Prefix: {address.FragmentPMBPrefix}")
        print(f"Fragment PMB Number: {address.FragmentPMBNumber}")
    print(f"Is CASS: {response.IsCASS}")
    else:
    print("\n* Error *\n")
    err = response.Error
    print(f"Error Type     : {err.Type}")
    print(f"Error Type Code: {err.TypeCode}")
    print(f"Error Desc     : {err.Desc}")
    print(f"Error Desc Code: {err.DescCode}")
```

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best address with parsed and corrected address information. This operation may return multiple address addresss if a single best match cannot be determined.

### [GetBestMatchesSingleLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## Library Usage

```
#
# Required fields:
#              Address
#              LicenseKey
#              IsLive
#
# Optional fields:
#              BusinessName
#
# Though the API will run in any scenario, not adhering to these
# rules can result in error responses.
```

# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly.  The inputs can be marginally incorrect, and this operation will correct them.  For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

### [ValidateCityStateZip Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-validatecitystatezip/)

## Library Usage

```
# 1. Build the input
#
# Required fields:
#              City (when PostalCode is missing)
#              State (when PostalCode is missing)
#              PostalCode (when City and state are missing)
#              LicenseKey
#              IsLive
#
# Though the API will run in any scenario, not adhering to these
# rules can result in error responses.
```

# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. The operation can be leveraged in conjunction with the GetBestMatches operation to find secondary numbers for input data that has either missing or incorrect unit information.

### [GetSecondaryNumbers Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getsecondarynumbers/)

## Library Usage

```
# 1. Build the input
#
# Required fields:
#              Address
#              City (when PostalCode is missing)
#              State (when PostalCode is missing)
#              PostalCode (when City and state are missing)
#              LicenseKey
#              IsLive
#
# Optional fields:
#              City (when PostalCode is present)
#              State (when PostalCode is present)
#              PostalCode (when City and state are present)
#
# Though the API will run in any scenario, not adhering to these
# rules can result in error responses.
```