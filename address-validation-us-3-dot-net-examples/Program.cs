// See https://aka.ms/new-console-template for more information
using address_validation_us_3_dot_net_examples;

//Your license key from Service Objects.
//Trial license keys will only work on the
//trail environments and production license
//keys will only owork on production environments.
string LicenseKey = "LICENSE KEY";

bool IsProductionKey = true;

//Address Validation US 3 - GetBestMatches - REST SDK
GetBestMatchesRestSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - GetBestMatches - SOAP SDK
GetBestMatchesSoapSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - SingleLine - REST SDK
GetBestMatchesSingleLineRestSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - SingleLine - SOAP SDK
GetBestMatchesSingleLineSoapSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - GetSecondaryNumbers - REST SDK
GetSecondaryNumbersRestSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - GetSecondaryNumbers - SOAP SDK
GetSecondaryNumbersSoapSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - ValidateCityStateZip - REST SDK
ValidateCityStateZipRestSdkExample.Go(LicenseKey, IsProductionKey);

//Address Validation US 3 - ValidateCityStateZip - SOAP SDK
ValidateCityStateZipSoapSdkExample.Go(LicenseKey, IsProductionKey);
