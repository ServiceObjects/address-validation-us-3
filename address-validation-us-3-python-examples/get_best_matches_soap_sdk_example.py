import sys
import os

sys.path.insert(0, os.path.abspath("../address-validation-us-3-python/SOAP"))

from get_best_matches_soap import GetBestMatchesValidation

def get_best_matches_soap_sdk_go(license, is_live_key):
    print("\n" + "-" * 51)
    print("Address Validation US 3 - GetBestMatches - SOAP SDK")
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
        service = GetBestMatchesValidation(license, is_live=is_live_key, timeout_ms=10000)
        response = service.get_best_matches(
            business_name="",
            address1="136 West Canon Perdido St",
            address2="Suite D",
            city="Santa Barbara",
            state="CA",
            postal_code="93101"
        )

        if not hasattr(response, 'Error'):
            print("\n* Validation *\n")
            for address in response.Addresses[0]:
                print(f"Address 1          : {address.Address1}")
                print(f"Address 2          : {address.Address2}")
                print(f"City               : {address.City}")
                print(f"State              : {address.State}")
                print(f"ZIP+4              : {address.Zip}")
                print(f"Is Residential     : {address.IsResidential}")
                print(f"DPV                : {address.DPV}")
                print(f"DPV Desc           : {address.DPVDesc}")
                print(f"DPV Notes          : {address.DPVNotes}")
                print(f"DPV Notes Desc     : {address.DPVNotesDesc}")
                print(f"Corrections        : {address.Corrections}")
                print(f"Corrections Desc   : {address.CorrectionsDesc}")
                print(f"Barcode Digits     : {address.BarcodeDigits}")
                print(f"Carrier Route      : {address.CarrierRoute}")
                print(f"Congress Code      : {address.CongressCode}")
                print(f"County Code        : {address.CountyCode}")
                print(f"County Name        : {address.CountyName}")
                print(f"Fragment House     : {address.FragmentHouse}")
                print(f"Fragment Pre Dir   : {address.FragmentPreDir}")
                print(f"Fragment Street    : {address.FragmentStreet}")
                print(f"Fragment Suffix    : {address.FragmentSuffix}")
                print(f"Fragment Post Dir  : {address.FragmentPostDir}")
                print(f"Fragment Unit      : {address.FragmentUnit}")
                print(f"Fragment           : {address.Fragment}")
                print(f"Fragment PMB Prefix: {address.FragmentPMBPrefix}")
                print(f"Fragment PMB Number: {address.FragmentPMBNumber}")
            print(f"Is CASS: {response.IsCASS}")
        else:
            print("\n* Error *\n")
            err = response.Error
            print(f"Error Type     : {err.Type}")
            print(f"Error Type Code: {err.TypeCode}")
            print(f"Error Desc     : {err.Desc}")
            print(f"Error Desc Code: {err.DescCode}")
    except RuntimeError as e:
        print("Error calling service:", e)
