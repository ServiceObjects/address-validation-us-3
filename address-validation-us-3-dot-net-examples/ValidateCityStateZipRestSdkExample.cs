using address_validation_us_3_dot_net.REST;

namespace address_validation_us_3_dot_net_examples
{
    internal static class ValidateCityStateZipRestSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n---------------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - ValidateCityStateZip - REST SDK");
            Console.WriteLine("---------------------------------------------------------");

            ValidateCityStateZipClient.ValidateCityStateZipInput validateCityStateZipInput = new(
                "Santa Barbara",
                "CA",
                "93101",
                LicenseKey,
                IsLive
            );
            Console.WriteLine("\r\n* Input *\r\n");

            Console.WriteLine($"City       : {validateCityStateZipInput.City}");
            Console.WriteLine($"State      : {validateCityStateZipInput.State}");
            Console.WriteLine($"ZIP Code   : {validateCityStateZipInput.Zip}");
            Console.WriteLine($"Is Live    : {validateCityStateZipInput.IsLive.ToString()}");
            Console.WriteLine($"License Key: {validateCityStateZipInput.LicenseKey}");


            CSZResponse response = ValidateCityStateZipClient.Invoke(validateCityStateZipInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Validation *\r\n");

                Console.WriteLine($"City                          : {response.CityStateZip.City}");
                Console.WriteLine($"State                         : {response.CityStateZip.State}");
                Console.WriteLine($"ZIP Code                      : {response.CityStateZip.Zip}");
                Console.WriteLine($"General Delivery Service      : {response.CityStateZip.GeneralDeliveryService}");
                Console.WriteLine($"Street Service                : {response.CityStateZip.StreetService}");
                Console.WriteLine($"PO Box Service                : {response.CityStateZip.POBoxService}");
                Console.WriteLine($"PO Box Range Low              : {response.CityStateZip.POBoxRangeLow}");
                Console.WriteLine($"PO Box Range High             : {response.CityStateZip.POBoxRangeHigh}");
                Console.WriteLine($"Rural Route / Highway Contract: {response.CityStateZip.RRHCService}");
                Console.WriteLine($"Urbanization Service          : {response.CityStateZip.UrbanizationService}");
                Console.WriteLine($"Is Unique ZIP Code            : {response.CityStateZip.IsUniqueZipCode}");
            }
            else
            {
                Console.WriteLine("\r\n* Error *\r\n");

                Console.WriteLine($"Error Type     : {response.Error.Type}");
                Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
                Console.WriteLine($"Error Desc     : {response.Error.Desc}");
                Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
            }
        }
    }
}
