import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/SOAP"))

from validate_city_state_zip_soap import ValidateCityStateZipValidation

def validate_city_state_zip_soap_sdk_go(license, is_live_key):
    print("\n" + "-" * 57)
    print("Address Validation US 3 - ValidateCityStateZip - SOAP SDK")
    print("-" * 57)

    print("\n* Input *\n")
    print(f"City       : Santa Barbara")
    print(f"State      : CA")
    print(f"ZIP Code   : 93101")
    print(f"Is Live    : {is_live_key}")
    print(f"License Key: {license}")

    try:
        service = ValidateCityStateZipValidation(license, is_live=is_live_key, timeout_ms=10000)
        response = service.validate_city_state_zip(
            city="Santa Barbara",
            state="CA",
            zip="93101"
        )

        if not hasattr(response, 'Error'):
            print("\n* Validation *\n")
            city_state_zip = response.CityStateZip
            print(f"City                  : {city_state_zip.City}")
            print(f"State                 : {city_state_zip.State}")
            print(f"ZIP                   : {city_state_zip.Zip}")             
            print(f"GeneralDeliveryService: {city_state_zip.GeneralDeliveryService}")
            print(f"POBoxService          : {city_state_zip.POBoxService}")
            print(f"StreetService         : {city_state_zip.StreetService}")
            print(f"RRHCService           : {city_state_zip.RRHCService}")
            print(f"UrbanizationService   : {city_state_zip.UrbanizationService}")
            print(f"POBoxRangeLow         : {city_state_zip.POBoxRangeLow}")
            print(f"POBoxRangeHigh        : {city_state_zip.POBoxRangeHigh}")
            print(f"IsUniqueZipCode       : {city_state_zip.IsUniqueZipCode}")
        else:
            print("\n* Error *\n")
            err = response.Error
            print(f"Error Type     : {err.Type}")
            print(f"Error Type Code: {err.TypeCode}")
            print(f"Error Desc     : {err.Desc}")
            print(f"Error Desc Code: {err.DescCode}")
    except RuntimeError as e:
        print("Error calling service:", e)
