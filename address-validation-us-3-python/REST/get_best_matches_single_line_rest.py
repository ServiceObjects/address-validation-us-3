'''
Service Objects, Inc.

This module provides the get_best_matches_single_line function to validate and standardize US addresses
using Service Objects AV3 API. This operation takes a single line of address information as the input and 
returns the best candidate with parsed and corrected address informationIt handles live/trial endpoints, 
fallback logic, and JSON parsing.

Functions:
    get_best_matches_single_line(business_name: str,
                     address: str,
                     license_key: str,
                     is_live: bool) -> dict:
'''  

import requests  # HTTP client for RESTful API calls

# Endpoint URLs for AV3 service
primary_url = 'https://sws.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?'
backup_url = 'https://swsbackup.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?'
trial_url = 'https://trial.serviceobjects.com/AV3/api.svc/GetBestMatchesSingleLineJson?'


def get_best_matches_single_line(business_name: str,
                   address: str,
                   license_key: str,
                   is_live: bool) -> dict:
    """
    Call AV3 get_best_matches_single_line API and return validation results.

    Parameters:
        business_name (str): Company name to assist suite parsing.
        address (str): Entire address to Validate. For example (123 Main Street, Anytown CA 99999). Required.
        license_key (str): Service Objects license key.
        is_live (bool): True for production endpoints, False for trial URL.

    Returns:
        dict: Parsed JSON response with address addresss or error info.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """

    # Prepare query parameters for AV3 API
    params = {
        'BusinessName': business_name,
        'Address'     : address,
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