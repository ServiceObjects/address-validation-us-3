using address_validation_us_3_dot_net.SOAP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static address_validation_us_3_dot_net.REST.GetBestMatchesClient;

namespace address_validation_us_3_dot_net_examples
{
    internal static class ValidateCityStateZipSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n---------------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - ValidateCityStateZip - SOAP SDK");
            Console.WriteLine("---------------------------------------------------------");

            Console.WriteLine("\r\n* Input *\r\n");

            Console.WriteLine($"City       : Santa Barbara");
            Console.WriteLine($"State      : CA");
            Console.WriteLine($"ZIP Code   : 93101");
            Console.WriteLine($"Is Live    : {IsLive.ToString()}");
            Console.WriteLine($"License Key: {LicenseKey}");


            CityStateZipValidation cityStateZipValidation = new(IsLive);
            AV3Service.CityStateZipResponse response = cityStateZipValidation.ValidateCityStateZip(
                "Santa Barbara",
                "CA",
                "93101",
                LicenseKey
            );

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
