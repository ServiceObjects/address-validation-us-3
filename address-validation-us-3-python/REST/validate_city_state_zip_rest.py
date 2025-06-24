'''
Service Objects, Inc.

This module provides the validate_city_state_zip function to validate a given city-state-zip combination
using Service Objects AV3 API. It handles live/trial endpoints, fallback logic, and JSON parsing.

Functions:
    validate_city_state_zip(city: str,
                     state: str,
                     zip: str,
                     license_key: str,
                     is_live: bool) -> dict:
'''  

import requests  # HTTP client for RESTful API calls

# Endpoint URLs for AV3 service
primary_url = 'https://sws.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?'
backup_url = 'https://swsbackup.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?'
trial_url = 'https://trial.serviceobjects.com/AV3/api.svc/ValidateCityStateZipJson?'


def validate_city_state_zip(city: str,
                   state: str,
                   zip: str,
                   license_key: str,
                   is_live: bool) -> dict:
    """
    Call AV3 get_best_matches API and return validation results.

    Parameters:
        city (str): City name; required if PostalCode not provided.
        state (str): State code or full name; required if PostalCode not provided.
        zip (str): 5- or 9-digit ZIP; required if City/State not provided.
        license_key (str): Service Objects license key.
        is_live (bool): True for production endpoints, False for trial URL.

    Returns:
        dict: Parsed JSON response with location or error info.
    """

    # Prepare query parameters for AV3 API
    params = {
        'City'        : city,
        'State'       : state,
        'ZIP'         : zip,
        'LicenseKey'  : license_key
    }

    # Select the base URL: production vs trial
    url = primary_url if is_live else trial_url

    # Attempt primary (or trial) endpoint first
    try:
        response = requests.get(url, params=params, timeout=10)
        data = response.json()

        # If API returned an error in JSON payload, trigger fallback
        error = getattr(response, 'Error', None)
        if not (error is None or getattr(error, 'TypeCode', None) != "3"):
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