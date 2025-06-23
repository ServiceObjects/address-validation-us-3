import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/REST"))

from get_secondary_numbers_rest import get_secondary_numbers

def get_secondary_numbers_rest_sdk_go(license, is_live_key):
    print("\n" + "-" * 51)
    print("Address Validation US 3 - GetSecondaryNumbers - REST SDK")
    print("-" * 51)

    print("\n* Input *\n")
    print(f"Address     : 136 West Canon Perdido St")
    print(f"City         : Santa Barbara")
    print(f"State        : CA")
    print(f"ZIP Code     : 93101")
    print(f"Is Live      : {is_live_key}")
    print(f"License Key  : {license}")

    try:
        response = get_secondary_numbers(
            address="136 West Canon Perdido St",
            city="Santa Barbara",
            state="CA",
            postal_code="93101",
            license_key=license, 
            is_live=True
        )

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
    except RuntimeError as e:
        print("Error calling service:", e)
