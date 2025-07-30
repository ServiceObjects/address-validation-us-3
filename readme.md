![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AV3 - Address Validation US 3

DOTS Address Validation 3 US (“AV3”) is V3 version of our Address Validation and Address Validation 2 Web services. This service utilizes the latest .Net Framework, WCF, and can be used as a RESTful service or with SOAP. AV3 is designed to take an unstandardized address, validate it against the latest USPS data, and return standardized, deliverable addresses. The service provides corrected information such as the correct street location and zip plus four code, along with parsed address tokens, such as the PMB box number, pre- and post-directionals, county and state codes, and much more.

AV3 can provide instant address verification and correction to websites or enhancement to contact lists.  However, the output from AV3 must be considered carefully before the existence or non-existence of an address is decided.

## [Service Objects Website](https://serviceobjects.com)
## [Developer Guide/Documentation](https://www.serviceobjects.com/docs/)

# AV3 - GetBestMatches

GetBestMatches: Returns parsed and validated address elements including Delivery Point Validation (DPV), Residential Delivery Indicator (RDI), and Suite data. GetBestMatches will attempt to validate the input address against a CASS approved engine, and make corrections where possible. Multiple address addresss may be returned if a definitive decision cannot be made by the service.

## [GetBestMatches Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatches-recommended/)

## GetBestMatches Request URLs (Query String Parameters)

>[!CAUTION]
>### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*


### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatches?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatches?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatches?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D&Address2=&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

# AV3 - GetBestMatchesSingleLine

Takes a single line of address information as the input and returns the best address with parsed and corrected address information. This operation may return multiple address addresss if a single best match cannot be determined.

## [GetBestMatchesSingleLine Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getbestmatchessingleline/)

## GetBestMatchesSingleLine Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLine?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLine?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLine?BusinessName=Service+Objects&Address=136+W+Canon+Perdido+St%2C+Suite+D+Santa+Barbara+CA+93101&LicenseKey={LicenseKey}

# AV3 - ValidateCityStateZip

This operation will validate that a given city-state-zip validate together properly.  The inputs can be marginally incorrect, and this operation will correct them.  For instance, a combination with a valid city, slightly misspelled state, and totally incorrect zip code will be corrected to a valid city – state – zip code combination.

## [ValidateCityStateZip Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-validatecitystatezip/)

## ValidateCityStateZip Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/ValidateCityStateZip?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/ValidateCityStateZip?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/ValidateCityStateZip?City=Asbury+Park&State=NJ&Zip=07712&LicenseKey={LicenseKey}

# AV3 - GetSecondaryNumbers

Returns parsed and validated address elements along with a list of potential secondary numbers for a given input address. The operation can be leveraged in conjunction with the GetBestMatches operation to find secondary numbers for input data that has either missing or incorrect unit information.

## [GetSecondaryNumbers Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-validation-us-3/av3-operations/av3-getsecondarynumbers/)

## GetSecondaryNumbers Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AV3/api.svc/GetSecondaryNumbers?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AV3/api.svc/GetSecondaryNumbers?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AV3/api.svc/GetSecondaryNumbers?Address=27+E+Cota+St&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}
