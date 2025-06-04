'''
Service Objects, Inc.

This module provides the get_secondary_numbers function to return parsed and validated
address elements along with a list of potential secondary numbers for a given input address. 
The operation can be leveraged in conjunction with the GetBestMatches operation to find 
secondary numbers for input data that has either missing or incorrect unit information. 

Functions:
    get_secondary_numbers(address: str,
                          city: str,
                          state: str,
                          postal_code: str,
                          license_key: str,
                          is_live: bool) -> dict:
'''  
import requests  # HTTP client for RESTful API calls

# Endpoint URLs for AV3 service
primary_url = 'https://sws.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?'
backup_url  = 'https://swsbackup.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?'
trial_url   = 'https://trial.serviceobjects.com/AV3/api.svc/GetSecondaryNumbersJson?'


def get_secondary_numbers(address: str,
                          city: str,
                          state: str,
                          postal_code: str,
                          license_key: str,
                          is_live: bool) -> dict:
    """
    Retrieve candidate secondary numbers (e.g. unit/suite) for a given U.S. address.

    This operation complements GetBestMatches by returning plausible secondary numbers
    when the input lacks or has incorrect unit information.

    Parameters:
        address (str): Primary street address (e.g., '123 Main St'). Required.
        city (str): City name; required if PostalCode is omitted.
        ctate (str): State code or full name; required if PostalCode is omitted.
        postal_code (str): 5‑ or 9‑digit ZIP; required if City/State are omitted.
        license_key (str): Your Service Objects AV3 license key.
        is_live (bool): True for production (primary→backup endpoints), False for trial.

    Returns:
        dict: JSON response with a list of secondary number candidates or error info.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """
    # Prepare query parameters for AV3 API
    params = {
        'Address': address,
        'City': city,
        'State': state,
        'PostalCode': postal_code,
        'LicenseKey': license_key
    }

    # Choose base URL: production or trial
    url = primary_url if is_live else trial_url

    try:
        # Primary (or trial) endpoint request
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()  # HTTP-level errors
        data = response.json()

        # Application-level error? Fallback if live.
        if 'Error' in data:
            if is_live:
                # TODO: Log fallback attempt
                response = requests.get(backup_url, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()
                if 'Error' in data:
                    raise RuntimeError(f"AV3 SecondaryNumbers error: {data['Error']}")
            else:
                raise RuntimeError(f"AV3 SecondaryNumbers trial error: {data['Error']}")

        return data

    except requests.RequestException as http_exc:
        # Network/HTTP failure
        if is_live:
            try:
                # Fallback to backup endpoint
                response = requests.get(backup_url, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()
                if 'Error' in data:
                    raise RuntimeError(f"AV3 SecondaryNumbers backup error: {data['Error']}")
                return data
            except Exception as backup_exc:
                # Both endpoints failed
                raise RuntimeError("AV3 SecondaryNumbers service unreachable") from backup_exc
        else:
            # Trial mode: propagate the original exception
            raise