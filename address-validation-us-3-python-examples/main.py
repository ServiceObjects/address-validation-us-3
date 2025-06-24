from get_best_matches_rest_sdk_example import get_best_matches_rest_sdk_go
from get_best_matches_soap_sdk_example import get_best_matches_soap_sdk_go
from get_secondary_numbers_rest_sdk_example import get_secondary_numbers_rest_sdk_go
from get_secondary_numbers_soap_sdk_example import get_secondary_numbers_soap_sdk_go
from get_best_matches_single_line_rest_sdk_example import get_best_matches_single_line_rest_sdk_go
from get_best_matches_single_line_soap_sdk_example import get_best_matches_single_line_soap_sdk_go
from validate_city_state_zip_rest_sdk_example import validate_city_state_zip_rest_sdk_go
from validate_city_state_zip_soap_sdk_example import validate_city_state_zip_soap_sdk_go




if __name__ == "__main__":
    
    # Your license key from Service Objects.
    # Trial license keys will only work on the
    # trail environments and production license
    # keys will only owork on production environments.
    license_key = "LICENSE KEY"

    is_live_license_key = True

    # Address Validation US 3 - GetBestMatches - REST SDK
    get_best_matches_rest_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - GetBestMatches - SOAP SDK
    get_best_matches_soap_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - GetSecondaryNumbers - REST SDK
    get_secondary_numbers_rest_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - GetSecondaryNumbers - SOAP SDK
    get_secondary_numbers_soap_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - GetBestMatchesSingleLine - REST SDK
    get_best_matches_single_line_rest_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - GetBestMatchesSingleLine - SOAP SDK
    get_best_matches_single_line_soap_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - ValidateCityStateZip - REST SDK
    validate_city_state_zip_rest_sdk_go(license_key, is_live_license_key)

    # Address Validation US 3 - ValidateCityStateZip - SOAP SDK
    validate_city_state_zip_soap_sdk_go(license_key, is_live_license_key)
