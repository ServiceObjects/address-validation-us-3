'''
Service Objects, Inc.

This module provides the get_best_matches function to validate and standardize US addresses
using Service Objects’ AV3 API. It handles live/trial endpoints, fallback logic, and JSON parsing.

Functions:
    get_best_matches(business_name: str,
                     address: str,
                     address_2: str,
                     city: str,
                     state: str,
                     postal_code: str,
                     license_key: str,
                     is_live: bool) -> dict:
'''  
import requests  # HTTP client for RESTful API calls

# Endpoint URLs for AV3 service
primary_url = 'https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?'
backup_url = 'https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?'
trial_url = 'https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesJson?'


def get_best_matches(business_name: str,
                   address: str,
                   address_2: str,
                   city: str,
                   state: str,
                   postal_code: str,
                   license_key: str,
                   is_live: bool) -> dict:
    """
    Call AV3 get_best_matches API and return validation results.

    Parameters:
        business_name (str): Company name to assist suite parsing.
        address (str): Primary street address (e.g., '123 Main St'). Required.
        address_2 (str): Secondary address info (e.g., 'C/O John Smith').
        city (str): City name; required if PostalCode not provided.
        state (str): State code or full name; required if PostalCode not provided.
        postal_code (str): 5- or 9-digit ZIP; required if City/State not provided.
        license_key (str): Service Objects license key.
        is_live (bool): True for production endpoints, False for trial URL.

    Returns:
        dict: Parsed JSON response with address candidates or error info.
    """

    # Prepare query parameters for AV3 API
    params = {
        'BusinessName': business_name,
        'Address'     : address,
        'Address2'    : address_2,
        'City'        : city,
        'State'       : state,
        'PostalCode'  : postal_code,
        'LicenseKey'  : license_key
    }

    # Select the base URL: production vs trial
    url = primary_url if is_live else trial_url

    # Attempt primary (or trial) endpoint first
    try:
        response = requests.get(url, params=params, timeout=10)
        data = response.json()

        # If API returned an error in JSON payload, trigger fallback
        if 'Error' in data:
            if is_live:
                # Try backup URL when live
                response = requests.get(backup_url, params=params, timeout=10)
                data = response.json()
                # If still error, propagate exception
                if 'Error' in data:
                    raise RuntimeError(f"AV3 service error: {data['Error']}")
            else:
                # Trial mode should not fallback; error is terminal
                raise RuntimeError(f"AV3 trial error: {data['Error']}")

        # Success: return parsed JSON data
        return data

    except requests.RequestException as req_exc:
        # Network or HTTP-level error occurred
        # TODO: Instrument alerting on repeated failures
        if is_live:
            try:
                # Fallback to backup URL on network failure
                response = requests.get(backup_url, params=params, timeout=10)
                data = response.json()
                if 'Error' in data:
                    raise RuntimeError(f"AV3 backup error: {data['Error']}")
                return data
            except Exception as backup_exc:
                # Both primary and backup failed; escalate
                raise RuntimeError("AV3 service unreachable on both endpoints") from backup_exc
        else:
            # In trial mode, propagate the network exception
            raise