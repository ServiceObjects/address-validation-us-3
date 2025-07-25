using address_validation_us_3_dot_net.SOAP;

namespace address_validation_us_3_dot_net_examples
{
    internal static class GetBestMatchesSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n---------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - GetBestMatches - SOAP SDK");
            Console.WriteLine("---------------------------------------------------");

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Business Name: ");
            Console.WriteLine($"Address 1    : 136 West Canon Perdido St");
            Console.WriteLine($"Address 2    : Suite D");
            Console.WriteLine($"City         : Santa Barbara");
            Console.WriteLine($"State        : CA");
            Console.WriteLine($"ZIP Code     : 93101");
            Console.WriteLine($"Is Live      : {IsLive.ToString()}");
            Console.WriteLine($"License Key  : {LicenseKey}");

            GetBestMatchesValidation getBestMatchesValidation = new(IsLive);
            AV3Service.BestMatchesResponse response = getBestMatchesValidation.GetBestMatches(
                "",
                "136 West Canon Perdido St",
                "Suite D",
                "Santa Barbara",
                "CA",
                "93101",
                LicenseKey
            ).Result;

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Validation *\r\n");

                foreach (AV3Service.Address address in response.Addresses)
                {
                    Console.WriteLine($"Address 1          : {address.Address1}");
                    Console.WriteLine($"Address 2          : {address.Address2}");
                    Console.WriteLine($"City               : {address.City}");
                    Console.WriteLine($"State              : {address.State}");
                    Console.WriteLine($"ZIP+4              : {address.Zip}");
                    Console.WriteLine($"Is Residential     : {address.IsResidential}");
                    Console.WriteLine($"DPV                : {address.DPV}");
                    Console.WriteLine($"DPV Desc           : {address.DPVDesc}");
                    Console.WriteLine($"DPV Notes          : {address.DPVNotes}");
                    Console.WriteLine($"DPV Notes Desc     : {address.DPVNotesDesc}");
                    Console.WriteLine($"Corrections        : {address.Corrections}");
                    Console.WriteLine($"Corrections Desc   : {address.CorrectionsDesc}");
                    Console.WriteLine($"Barcode Digits     : {address.BarcodeDigits}");
                    Console.WriteLine($"Carrier Route      : {address.CarrierRoute}");
                    Console.WriteLine($"Congress Code      : {address.CongressCode}");
                    Console.WriteLine($"County Code        : {address.CountyCode}");
                    Console.WriteLine($"County Name        : {address.CountyName}");
                    Console.WriteLine($"Fragment House     : {address.FragmentHouse}");
                    Console.WriteLine($"Fragment Pre Dir   : {address.FragmentPreDir}");
                    Console.WriteLine($"Fragment Street    : {address.FragmentStreet}");
                    Console.WriteLine($"Fragment Suffix    : {address.FragmentSuffix}");
                    Console.WriteLine($"Fragment Post Dir  : {address.FragmentPostDir}");
                    Console.WriteLine($"Fragment Unit      : {address.FragmentUnit}");
                    Console.WriteLine($"Fragment           : {address.Fragment}");
                    Console.WriteLine($"Fragment PMB Prefix: {address.FragmentPMBPrefix}");
                    Console.WriteLine($"Fragment PMB Number: {address.FragmentPMBNumber}");
                }
                Console.WriteLine($"Is CASS: {response.IsCASS}");
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
