import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/REST"))

from validate_city_state_zip_rest import validate_city_state_zip

def validate_city_state_zip_rest_sdk_go(license, is_live_key):
    print("\n" + "-" * 57)
    print("Address Validation US 3 - ValidateCityStateZip - REST SDK")
    print("-" * 57)

    print("\n* Input *\n")
    print(f"City       : Santa Barbara")
    print(f"State      : CA")
    print(f"ZIP Code   : 93101")
    print(f"Is Live    : {is_live_key}")
    print(f"License Key: {license}")

    try:
        response = validate_city_state_zip(
            city="Santa Barbara",
            state="CA",
            zip="93101",
            license_key=license, 
            is_live=is_live_key
        )

        if not response.get('Error'):
            print("\n* Validation *\n")
            city_state_zip = response.get('CityStateZip', {})
            if city_state_zip:
                print(f"City                  : {city_state_zip.get('City', '')}")
                print(f"State                 : {city_state_zip.get('State', '')}")
                print(f"ZIP                   : {city_state_zip.get('Zip', '')}")
                print(f"GeneralDeliveryService: {city_state_zip.get('GeneralDeliveryService', '')}")
                print(f"POBoxService          : {city_state_zip.get('POBoxService', '')}")
                print(f"StreetService         : {city_state_zip.get('StreetService', '')}")
                print(f"RRHCService           : {city_state_zip.get('RRHCService', '')}")
                print(f"UrbanizationService   : {city_state_zip.get('UrbanizationService', '')}")
                print(f"POBoxRangeLow         : {city_state_zip.get('POBoxRangeLow', '')}")
                print(f"POBoxRangeHigh        : {city_state_zip.get('POBoxRangeHigh', '')}")
                print(f"IsUniqueZipCode       : {city_state_zip.get('IsUniqueZipCode', '')}")
            else:
                print("\n* Error *\n")
                err = response['Error']
                print(f"Error Type     : {err.get('Type', '')}")
                print(f"Error Type Code: {err.get('TypeCode', '')}")
                print(f"Error Desc     : {err.get('Desc', '')}")
                print(f"Error Desc Code: {err.get('DescCode', '')}")
    except RuntimeError as e:
        print("Error calling service:", e)
