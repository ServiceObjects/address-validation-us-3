import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/SOAP"))

from get_secondary_numbers_soap import GetSecondaryNumbersValidation

def get_secondary_numbers_soap_sdk_go(license, is_live_key):
    print("\n" + "-" * 56)
    print("Address Validation US 3 - GetSecondaryNumbers - SOAP SDK")
    print("-" * 56)

    print("\n* Input *\n")
    print(f"Address    : 136 West Canon Perdido St")
    print(f"City       : Santa Barbara")
    print(f"State      : CA")
    print(f"ZIP Code   : 93101")
    print(f"Is Live    : {is_live_key}")
    print(f"License Key: {license}")

    
    try:
        service = GetSecondaryNumbersValidation(license, is_live=is_live_key, timeout_ms=10000)
        response = service.get_secondary_numbers(
            address="136 West Canon Perdido St",
            city="Santa Barbara",
            state="CA",
            postal_code="93101"
        )

        if not hasattr(response, 'Error'):
            print("\n* Validation *\n")
            print(f"Address 1       : {response.Address1}")
            print(f"City            : {response.City}")
            print(f"State           : {response.State}")
            print(f"ZIP+4           : {response.Zip}")
            print(f"Total Count     : {response.TotalCount}")
            print(f"SecondaryNumbers: {', '.join(response.SecondaryNumbers.string)}")
        else:
            print("\n* Error *\n")
            err = response.Error
            print(f"Error Type     : {err.Type}")
            print(f"Error Type Code: {err.TypeCode}")
            print(f"Error Desc     : {err.Desc}")
            print(f"Error Desc Code: {err.DescCode}")
    except RuntimeError as e:
        print("Error calling service:", e)
