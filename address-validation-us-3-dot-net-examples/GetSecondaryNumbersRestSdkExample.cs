using address_validation_us_3_dot_net.REST;

namespace address_validation_us_3_dot_net_examples
{
    internal static class GetSecondaryNumbersRestSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n--------------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - GetSecondaryNumbers - REST SDK");
            Console.WriteLine("--------------------------------------------------------");

            GetSecondaryNumbersClient.GetSecondaryNumbersInput getSecondaryNumbersInput = new(
                "136 West Canon Perdido St",
                "Santa Barbara",
                "CA",
                "93101",
                LicenseKey,
                IsLive
            );
            Console.WriteLine("\r\n* Input *\r\n");

            Console.WriteLine($"Address    : {getSecondaryNumbersInput.Address}");
            Console.WriteLine($"City       : {getSecondaryNumbersInput.City}");
            Console.WriteLine($"State      : {getSecondaryNumbersInput.State}");
            Console.WriteLine($"ZIP Code   : {getSecondaryNumbersInput.PostalCode}");
            Console.WriteLine($"Is Live    : {getSecondaryNumbersInput.IsLive.ToString()}");
            Console.WriteLine($"License Key: {getSecondaryNumbersInput.LicenseKey}");


            GSNResponse response = GetSecondaryNumbersClient.Invoke(getSecondaryNumbersInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Validation *\r\n");

                Console.WriteLine($"Address          : {response.Address1}");
                Console.WriteLine($"City             : {response.City}");
                Console.WriteLine($"State            : {response.State}");
                Console.WriteLine($"ZIP+4            : {response.Zip}");
                Console.WriteLine($"Total Unit Count : {response.TotalCount.ToString()}");
                Console.WriteLine($"Secondary Numbers:");


                foreach (string unit in response.SecondaryNumbers)
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
