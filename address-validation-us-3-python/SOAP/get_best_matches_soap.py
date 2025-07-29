from suds.client import Client
from suds import WebFault
from suds.sudsobject import Object

class GetBestMatchesValidation:
    def __init__(self, license_key: str, is_live: bool, timeout_ms: int = 10000):
        """
        license_key: Service Objects Address Validation US - 3 license key.
        is_live: whether to use live or trial endpoints
        timeout_ms: SOAP call timeout in milliseconds
        """
        self._timeout_s = timeout_ms / 1000.0
        self._is_live = is_live
        self.license_key = license_key

        # WSDL URLs
        self._primary_wsdl = (
            "https://sws.serviceobjects.com/AV3/api.svc?wsdl"
            if is_live else
            "https://trial.serviceobjects.com/AV3/api.svc?wsdl"
        )
        self._backup_wsdl = (
            "https://swsbackup.serviceobjects.com/AV3/api.svc?wsdl"
            if is_live else
            "https://trial.serviceobjects.com/AV3/api.svc?wsdl"
        )

    def get_best_matches(self,
                         business_name: str,
                         address1: str,
                         address2: str,
                         city: str,
                         state: str,
                         postal_code: str) -> Object:
        """
        Calls GetBestMatches on the primary endpoint; on None response,
        WebFault, or Error.TypeCode == '3' falls back to the backup endpoint.
        :returns: the suds response object
        :raises RuntimeError: if both endpoints fail
        """
        # Common kwargs for both calls
        call_kwargs = dict(
            BusinessName=business_name,
            Address=address1,
            Address2=address2,
            City=city,
            State=state,
            PostalCode=postal_code,
            LicenseKey=self.license_key
        )

        # Attempt primary
        try:
            client = Client(self._primary_wsdl, timeout=self._timeout_s)

            # Override endpoint URL if needed:
            response = client.service.GetBestMatches(**call_kwargs)

            # If response is None or fatal error code, trigger fallback
            if response is None or (hasattr(response, 'Error') and response.Error and response.Error.TypeCode == '3'):
                raise ValueError("Primary returned no result or fatal Error.TypeCode=3")

            return response

        except (WebFault, ValueError, Exception) as primary_ex:
            # Attempt backup
            try:
                client = Client(self._backup_wsdl, timeout=self._timeout_s)
                response = client.service.GetBestMatches(**call_kwargs)
                if response is None:
                    raise ValueError("Backup returned no result")
                return response

            except (WebFault, Exception) as backup_ex:
                msg = (
                    "Both primary and backup endpoints failed.\n"
                    f"Primary error: {primary_ex}\n"
                    f"Backup error: {backup_ex}"
                )
                raise RuntimeError(msg)