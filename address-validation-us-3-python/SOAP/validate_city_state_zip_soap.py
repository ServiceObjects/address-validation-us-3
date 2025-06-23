from suds.client import Client
from suds import WebFault
from suds.sudsobject import Object

class ValidateCityStateZipValidation:
    def __init__(self, license_key: str, is_live: bool, timeout_ms: int = 10000):
        """
        :param is_live: whether to use live or trial endpoints
        :param timeout_ms: SOAP call timeout in milliseconds
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

    def validate_city_state_zip(self,
                         city: str,
                         state: str,
                         zip: str) -> Object:
        """
        Calls ValidateCityStateZip on the primary endpoint; on None response,
        WebFault, or Error.TypeCode == '3' falls back to the backup endpoint.
        :returns: the suds response object
        :raises RuntimeError: if both endpoints fail
        """
        # Common kwargs for both calls
        call_kwargs = dict(
            City=city,
            State=state,
            Zip=zip,
            LicenseKey=self.license_key
        )

        # Attempt primary
        try:
            client = Client(self._primary_wsdl, timeout=self._timeout_s)
            # Override endpoint URL if needed:
            # client.set_options(location=self._primary_wsdl.replace('?wsdl','/soap'))
            response = client.service.ValidateCityStateZip(**call_kwargs)

            # If response is None or fatal error code, trigger fallback
            if response is None or (hasattr(response, 'Error') and response.Error and response.Error.TypeCode == '3'):
                raise ValueError("Primary returned no result or fatal Error.TypeCode=3")

            return response

        except (WebFault, ValueError, Exception) as primary_ex:
            # Attempt backup
            try:
                client = Client(self._backup_wsdl, timeout=self._timeout_s)
                response = client.service.ValidateCityStateZip(**call_kwargs)
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