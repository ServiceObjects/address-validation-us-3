# av3-address-validation-GetBestMatches
GetBestMatches: CASS‑approved US address validation &amp; standardization (DPV, RDI, suite data, address validation, parsing)

# AV3 Address Validation GetBestMatches

A lightweight Python client for Service Objects’ DOTS Address Validation 3 US (“AV3”) API.  
Provides the `GetBestMatches` function to validate and standardize U.S. addresses (DPV, RDI, suite data), with built‑in live/trial endpoint selection and automatic fallback.

---

## Features

- **CASS‑approved** address validation against the USPS database  
- Returns standardized street, ZIP+4, county & state codes, PMB, suite data, and more  
- Delivery Point Validation (DPV) and Residential Delivery Indicator (RDI) flags  
- Automatic fallback from primary → backup (live mode)  
- Seamless trial vs. production endpoint switching  
- JSON parsing into native Python `dict` - Address fragments
- DPV Note Codes
- Address Correction Codes.

---
## [Service Objects Website](https://serviceobjects.com)
## [Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatches-recommended/)
## [License](LICENSE)

## Usage

```python
from av3_get_best_matches import GetBestMatches

# Example parameters
business_name = "Acme Corp"
address       = "123 Main St"
address_2     = ""
city          = "Los Angeles"
state         = "CA"
postal_code   = "90012"
license_key   = "YOUR_LICENSE_KEY"
is_live       = True

# This call will internally construct the URL:
# https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?
#   BusinessName=Acme%20Corp&
#   Address=123%20Main%20St&
#   Address2=&
#   City=Los%20Angeles&
#   State=CA&
#   PostalCode=90012&
#   LicenseKey=YOUR_LICENSE_KEY
results = GetBestMatches(
    business_name=business_name,
    address=address,
    address_2=address_2,
    city=city,
    state=state,
    postal_code=postal_code,
    license_key=license_key,
    is_live=is_live
)

print(results)