import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/REST"))

from get_best_matches_rest import get_best_matches

def get_best_matches_rest_sdk_go(license, is_live_key):
    print("\n" + "-" * 51)
    print("Address Validation US 3 - GetBestMatches - REST SDK")
    print("-" * 51)

    print("\n* Input *\n")
    print(f"Business Name: ")
    print(f"Address 1    : 136 West Canon Perdido St")
    print(f"Address 2    : Suite D")
    print(f"City         : Santa Barbara")
    print(f"State        : CA")
    print(f"ZIP Code     : 93101")
    print(f"Is Live      : {is_live_key}")
    print(f"License Key  : {license}")

    try:
        response = get_best_matches(
            business_name="",
            address="136 West Canon Perdido St",
            address_2="Suite D",
            city="Santa Barbara",
            state="CA",
            postal_code="93101",
            license_key=license, 
            is_live=is_live_key
        )

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
    except RuntimeError as e:
        print("Error calling service:", e)
