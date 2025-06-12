using address_validation_us_3_dot_net.SOAP;


namespace address_validation_us_3_dot_net_examples
{
    internal static class GetBestMatchesSingleLineSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        { 
            Console.WriteLine("\r\n------------------------------------------------");
            Console.WriteLine("Address Validation US 3 - SingleLine - SOAP SDK");
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("\r\n* Input *\r\n");

            Console.WriteLine($"Business Name: ");
            Console.WriteLine($"Full Address : 136 West Canon Perdido St, Suite D, Santa Barbara, CA 93101");
            Console.WriteLine($"Is Live      : {IsLive.ToString()}");
            Console.WriteLine($"License Key  : {LicenseKey}");

            GetBestMatchesSingleLineValidation getBestMatchesSingleLineValidation = new(IsLive);
            AV3Service.BestMatchesResponse response = getBestMatchesSingleLineValidation.GetBestMatchesSingleLine(
                "",
                "136 West Canon Perdido St, Suite D, Santa Barbara, CA 93101",
                LicenseKey
            );

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Validation *\r\n");

                foreach (AV3Service.Address candidate in response.Addresses)
                {
                    Console.WriteLine($"Address 1          : {candidate.Address1}");
                    Console.WriteLine($"Address 2          : {candidate.Address2}");
                    Console.WriteLine($"City               : {candidate.City}");
                    Console.WriteLine($"State              : {candidate.State}");
                    Console.WriteLine($"ZIP+4              : {candidate.Zip}");
                    Console.WriteLine($"Is Residential     : {candidate.IsResidential}");
                    Console.WriteLine($"DPV                : {candidate.DPV}");
                    Console.WriteLine($"DPV Desc           : {candidate.DPVDesc}");
                    Console.WriteLine($"DPV Notes          : {candidate.DPVNotes}");
                    Console.WriteLine($"DPV Notes Desc     : {candidate.DPVNotesDesc}");
                    Console.WriteLine($"Corrections        : {candidate.Corrections}");
                    Console.WriteLine($"Corrections Desc   : {candidate.CorrectionsDesc}");
                    Console.WriteLine($"Barcode Digits     : {candidate.BarcodeDigits}");
                    Console.WriteLine($"Carrier Route      : {candidate.CarrierRoute}");
                    Console.WriteLine($"Congress Code      : {candidate.CongressCode}");
                    Console.WriteLine($"County Code        : {candidate.CountyCode}");
                    Console.WriteLine($"County Name        : {candidate.CountyName}");
                    Console.WriteLine($"Fragment House     : {candidate.FragmentHouse}");
                    Console.WriteLine($"Fragment Pre Dir   : {candidate.FragmentPreDir}");
                    Console.WriteLine($"Fragment Street    : {candidate.FragmentStreet}");
                    Console.WriteLine($"Fragment Suffix    : {candidate.FragmentSuffix}");
                    Console.WriteLine($"Fragment Post Dir  : {candidate.FragmentPostDir}");
                    Console.WriteLine($"Fragment Unit      : {candidate.FragmentUnit}");
                    Console.WriteLine($"Fragment           : {candidate.Fragment}");
                    Console.WriteLine($"Fragment PMB Prefix: {candidate.FragmentPMBPrefix}");
                    Console.WriteLine($"Fragment PMB Number: {candidate.FragmentPMBNumber}");
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
