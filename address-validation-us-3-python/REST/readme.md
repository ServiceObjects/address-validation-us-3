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

from get_best_matches_rest import get_best_matches

# 2. Call the validation method.
response = get_best_matches(
            business_name="",
            address="136 West Canon Perdido St",
            address_2="Suite D",
            city="Santa Barbara",
            state="CA",
            postal_code="93101",
            license_key=license, 
            is_live=True
        )

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Validation *\n")
    # response['Addresses'] is a list of address-dicts
    for address in response.get('Addresses', []):
        print(f"Address 1          : {address.get('Address1', '')}")
        print(f"Address 2          : {address.get('Address2', '')}")
        print(f"City               : {address.get('City', '')}")
        print(f"State              : {address.get('State', '')}")
        print(f"ZIP+4              : {address.get('Zip', '')}")
        print(f"Is Residential     : {address.get('IsResidential', '')}")
        print(f"DPV                : {address.get('DPV', '')}")
        print(f"DPV Desc           : {address.get('DPVDesc', '')}")
        print(f"DPV Notes          : {address.get('DPVNotes', '')}")
        print(f"DPV Notes Desc     : {address.get('DPVNotesDesc', '')}")
        print(f"Corrections        : {address.get('Corrections', '')}")
        print(f"Corrections Desc   : {address.get('CorrectionsDesc', '')}")
        print(f"Barcode Digits     : {address.get('BarcodeDigits', '')}")
        print(f"Carrier Route      : {address.get('CarrierRoute', '')}")
        print(f"Congress Code      : {address.get('CongressCode', '')}")
        print(f"County Code        : {address.get('CountyCode', '')}")
        print(f"County Name        : {address.get('CountyName', '')}")
        print(f"Fragment House     : {address.get('FragmentHouse', '')}")
        print(f"Fragment Pre Dir   : {address.get('FragmentPreDir', '')}")
        print(f"Fragment Street    : {address.get('FragmentStreet', '')}")
        print(f"Fragment Suffix    : {address.get('FragmentSuffix', '')}")
        print(f"Fragment Post Dir  : {address.get('FragmentPostDir', '')}")
        print(f"Fragment Unit      : {address.get('FragmentUnit', '')}")
        print(f"Fragment           : {address.get('Fragment', '')}")
        print(f"Fragment PMB Prefix: {address.get('FragmentPMBPrefix', '')}")
        print(f"Fragment PMB Number: {address.get('FragmentPMBNumber', '')}")
    print(f"Is CASS: {response.get('IsCASS', '')}")
else:
    print("\n* Error *\n")
    err = response['Error']
    print(f"Error Type     : {err.get('Type', '')}")
    print(f"Error Type Code: {err.get('TypeCode', '')}")
    print(f"Error Desc     : {err.get('Desc', '')}")
    print(f"Error Desc Code: {err.get('DescCode', '')}")
```

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best address with parsed and corrected address information. This operation may return multiple address addresss if a single best match cannot be determined.

### [GetBestMatchesSingleLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## Library Usage

```
# 1. Build the input.
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

from get_best_matches_single_line_rest import get_best_matches_single_line

# 2. Call the validation method.
response = get_best_matches_single_line(
    business_name="",
    address="136 West Canon Perdido St Suite D, Santa Barbara, CA 93101",
    license_key=license, 
    is_live=True
)

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Validation *\n")
    # response['Addresses'] is a list of address-dicts
    for address in response.get('Addresses', []):
        print(f"Address 1          : {address.get('Address1', '')}")
        print(f"Address 2          : {address.get('Address2', '')}")
        print(f"City               : {address.get('City', '')}")
        print(f"State              : {address.get('State', '')}")
        print(f"ZIP+4              : {address.get('Zip', '')}")
        print(f"Is Residential     : {address.get('IsResidential', '')}")
        print(f"DPV                : {address.get('DPV', '')}")
        print(f"DPV Desc           : {address.get('DPVDesc', '')}")
        print(f"DPV Notes          : {address.get('DPVNotes', '')}")
        print(f"DPV Notes Desc     : {address.get('DPVNotesDesc', '')}")
        print(f"Corrections        : {address.get('Corrections', '')}")
        print(f"Corrections Desc   : {address.get('CorrectionsDesc', '')}")
        print(f"Barcode Digits     : {address.get('BarcodeDigits', '')}")
        print(f"Carrier Route      : {address.get('CarrierRoute', '')}")
        print(f"Congress Code      : {address.get('CongressCode', '')}")
        print(f"County Code        : {address.get('CountyCode', '')}")
        print(f"County Name        : {address.get('CountyName', '')}")
        print(f"Fragment House     : {address.get('FragmentHouse', '')}")
        print(f"Fragment Pre Dir   : {address.get('FragmentPreDir', '')}")
        print(f"Fragment Street    : {address.get('FragmentStreet', '')}")
        print(f"Fragment Suffix    : {address.get('FragmentSuffix', '')}")
        print(f"Fragment Post Dir  : {address.get('FragmentPostDir', '')}")
        print(f"Fragment Unit      : {address.get('FragmentUnit', '')}")
        print(f"Fragment           : {address.get('Fragment', '')}")
        print(f"Fragment PMB Prefix: {address.get('FragmentPMBPrefix', '')}")
        print(f"Fragment PMB Number: {address.get('FragmentPMBNumber', '')}")
    print(f"Is CASS: {response.get('IsCASS', '')}")
else:
    print("\n* Error *\n")
    err = response['Error']
    print(f"Error Type     : {err.get('Type', '')}")
    print(f"Error Type Code: {err.get('TypeCode', '')}")
    print(f"Error Desc     : {err.get('Desc', '')}")
    print(f"Error Desc Code: {err.get('DescCode', '')}")
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

from validate_city_state_zip_rest import validate_city_state_zip

# 2. Call the validation method.
response = validate_city_state_zip(
    city="Santa Barbara",
    state="CA",
    zip="93101",
    license_key=license, 
    is_live=True
)

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Validation *\n")
    city_state_zip = response.get('CityStateZip', {})
    if city_state_zip:
        print(f"City                        : {city_state_zip.get('City', '')}")
        print(f"State                       : {city_state_zip.get('State', '')}")
        print(f"ZIP                         : {city_state_zip.get('Zip', '')}")
        print(f"GeneralDeliveryService      : {city_state_zip.get('GeneralDeliveryService', '')}")
        print(f"POBoxService                : {city_state_zip.get('POBoxService', '')}")
        print(f"StreetService               : {city_state_zip.get('StreetService', '')}")
        print(f"RRHCService                 : {city_state_zip.get('RRHCService', '')}")
        print(f"UrbanizationService         : {city_state_zip.get('UrbanizationService', '')}")
        print(f"POBoxRangeLow               : {city_state_zip.get('POBoxRangeLow', '')}")
        print(f"POBoxRangeHigh              : {city_state_zip.get('POBoxRangeHigh', '')}")
        print(f"IsUniqueZipCode             : {city_state_zip.get('IsUniqueZipCode', '')}")
    else:
        print("\n* Error *\n")
        err = response['Error']
        print(f"Error Type     : {err.get('Type', '')}")
        print(f"Error Type Code: {err.get('TypeCode', '')}")
        print(f"Error Desc     : {err.get('Desc', '')}")
        print(f"Error Desc Code: {err.get('DescCode', '')}")
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

from get_secondary_numbers_rest import get_secondary_numbers

# 2. Call the validation method.
response = get_secondary_numbers(
            address="136 West Canon Perdido St",
            city="Santa Barbara",
            state="CA",
            postal_code="93101",
            license_key=license, 
            is_live=True
        )

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Validation *\n")
    print(f"Address 1          : {response.get('Address1', '')}")
    print(f"City               : {response.get('City', '')}")
    print(f"State              : {response.get('State', '')}")
    print(f"ZIP+4              : {response.get('Zip', '')}")
    print(f"Total Count        : {response.get('TotalCount', '')}")
    print(f"SecondaryNumbers   : {', '.join(response.get('SecondaryNumbers', []))}")
else:
    print("\n* Error *\n")
    err = response['Error']
    print(f"Error Type     : {err.get('Type', '')}")
    print(f"Error Type Code: {err.get('TypeCode', '')}")
    print(f"Error Desc     : {err.get('Desc', '')}")
    print(f"Error Desc Code: {err.get('DescCode', '')}")
```