using address_validation_us_3_dot_net.SOAP;

namespace address_validation_us_3_dot_net_examples
{
    internal static class GetSecondaryNumbersSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n--------------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - GetSecondaryNumbers - SOAP SDK");
            Console.WriteLine("--------------------------------------------------------");

            Console.WriteLine("\r\n* Input *\r\n");

            Console.WriteLine($"Address      : 136 West Canon Perdido St");
            Console.WriteLine($"City         : Santa Barbara");
            Console.WriteLine($"State        : CA");
            Console.WriteLine($"ZIP Code     : 93101");
            Console.WriteLine($"Is Live      : {IsLive.ToString()}");
            Console.WriteLine($"License Key  : {LicenseKey}");


            GetSecondaryNumbersValidation getSecondaryNumbersValidation = new(IsLive);
            AV3Service.SecondaryNumbersResponse response = getSecondaryNumbersValidation.GetSecondaryNumbers(
                "136 West Canon Perdido St",
                "Santa Barbara",
                "CA",
                "93101",
                LicenseKey
            ).Result;

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Validation *\r\n");

                Console.WriteLine($"Address          : {response.Address1}");
                Console.WriteLine($"City             : {response.City}");
                Console.WriteLine($"State            : {response.State}");
                Console.WriteLine($"ZIP+4            : {response.Zip}");
                Console.WriteLine($"Total Unit Count : {response.TotalCount.ToString()}");
                Console.WriteLine($"Secondary Numbers:");

                foreach(string unit in response.SecondaryNumbers)
                {
                    Console.WriteLine($"                  {unit}");
                }
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
