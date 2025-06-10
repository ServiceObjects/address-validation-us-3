using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace address_validation_us_3_dot_net.REST
{
    /// <summary>
    /// Response object for capturing the reponse from the API for GetBestMatches
    /// </summary>
    [DataContract]
    public class GBMResponse : IAV3Response
    {
        [DataMember(Name = "Addresses")]
        public Address[] Addresses { get; set; }
        [DataMember(Name = "IsCASS")]
        public bool IsCASS { get; set; }
        [DataMember(Name = "Error")]
        public Error Error { get; set; }
        public override string ToString()
        {
            string Output = "";
            Output += $"GBM Response:\n";
            Output += $"List of all Addresses: [";
            if (Addresses != null && Addresses.Length > 0)
            {
                foreach (Address A in Addresses)
                {
                    Output += A.ToString() + "\n";
                }
            }
            Output += "]\n";
            Output += $"IsCASS: {IsCASS}\n";
            Output += $"Error: {{{Error}}}\n";
            return Output;
        }

    }

    /// <summary>
    /// Response object for capturing the reponse from the API for GetSecondaryNumbers
    /// </summary>
    public class GSNResponse : IAV3Response
    {
        [DataMember(Name = "Address1")]
        public string Address1 { get; set; }
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "State")]
        public string State { get; set; }
        [DataMember(Name = "Zip")]
        public string Zip { get; set; }
        [DataMember(Name = "TotalCount")]
        public int TotalCount { get; set; }
        [DataMember(Name = "SecondaryNumbers")]
        public string[] SecondaryNumbers { get; set; }

        [DataMember(Name = "Error")]
        public Error Error { get; set; }

        public override string ToString()
        {
            string Output = "";
            Output += $"GSN Response:\n";
            Output += $"Address1: {Address1}\n";
            Output += $"City: {City}\n";
            Output += $"State: {State}\n";
            Output += $"Zip: {Zip}\n";
            Output += $"Total Count: {TotalCount}\n";
            Output += $"SecondaryNumbers: [";
            if (SecondaryNumbers != null && SecondaryNumbers.Length > 0)
            {
                foreach (string A in SecondaryNumbers)

                {
                    Output += A.ToString() + "\n";
                }
            }
            Output += "]\n";
            Output += $"Error: {Error}\n";
            return Output;
        }
    }


    /// <summary>
    /// Response object for capturing the reponse from the API for CityStateZip
    /// </summary>
    public class CSZResponse : IAV3Response
    {
        [DataMember(Name = "CityStateZip")]
        public CityStateZip CityStateZip { get; set; }

        [DataMember(Name = "Error")]
        public Error Error { get; set; }
        public override string ToString()
        {
            return $"CSZ Response:\n" +
                $"CityStateZip: {{{CityStateZip}}}\n" +
                $"Error: {{{Error}}}\n";
        }
    }

    /// <summary>
    /// Object to house the addresses being returned.
    /// </summary>
    public class Address
    {
        [DataMember(Name = "Address1")]
        public string Address1 { get; set; }
        [DataMember(Name = "Address2")]
        public string Address2 { get; set; }
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "State")]
        public string State { get; set; }
        [DataMember(Name = "Zip")]
        public string Zip { get; set; }
        [DataMember(Name = "IsResidential")]
        public string IsResidential { get; set; }
        [DataMember(Name = "DPV")]
        public string DPV { get; set; }
        [DataMember(Name = "DPVDesc")]
        public string DPVDesc { get; set; }
        [DataMember(Name = "DPVNotes")]
        public string DPVNotes { get; set; }
        [DataMember(Name = "DPVNotesDesc")]
        public string DPVNotesDesc { get; set; }
        [DataMember(Name = "Corrections")]
        public string Corrections { get; set; }
        [DataMember(Name = "CorrectionsDesc")]
        public string CorrectionsDesc { get; set; }
        [DataMember(Name = "BarcodeDigits")]
        public string BarcodeDigits { get; set; }
        [DataMember(Name = "CarrierRoute")]
        public string CarrierRoute { get; set; }
        [DataMember(Name = "CongressCode")]
        public string CongressCode { get; set; }
        [DataMember(Name = "CountyCode")]
        public string CountyCode { get; set; }
        [DataMember(Name = "CountyName")]
        public string CountyName { get; set; }
        [DataMember(Name = "FragmentHouse")]
        public string FragmentHouse { get; set; }
        [DataMember(Name = "FragmentPreDir")]
        public string FragmentPreDir { get; set; }
        [DataMember(Name = "FragmentStreet")]
        public string FragmentStreet { get; set; }
        [DataMember(Name = "FragmentSuffix")]
        public string FragmentSuffix { get; set; }
        [DataMember(Name = "FragmentPostDir")]
        public string FragmentPostDir { get; set; }
        [DataMember(Name = "FragmentUnit")]
        public string FragmentUnit { get; set; }
        [DataMember(Name = "Fragment")]
        public string Fragment { get; set; }
        [DataMember(Name = "FragmentPMBPrefix")]
        public string FragmentPMBPrefix { get; set; }
        [DataMember(Name = "FragmentPMBNumber")]
        public string FragmentPMBNumber { get; set; }
        public override string ToString()
        {
            return $"\n{{Address1: {Address1}\n" +
                $"\tAddress2: {Address2}\n" +
                $"\tCity: {City}\n" +
                $"\tState: {State}\n" +
                $"\tZip: {Zip}\n" +
                $"\tIsResidential: {IsResidential}\n" +
                $"\tDPV: {DPV}\n" +
                $"\tDPVDesc: {DPVDesc}\n" +
                $"\tDPVNotes: {DPVNotes}\n" +
                $"\tDPVNotesDesc: {DPVNotesDesc}\n" +
                $"\tCorrections: {Corrections}\n" +
                $"\tCorrectionsDesc: {CorrectionsDesc}\n" +
                $"\tBarcodeDigits: {BarcodeDigits}\n" +
                $"\tCarrierRoute: {CarrierRoute}\n" +
                $"\tCongressCode: {CongressCode}\n" +
                $"\tCountyCode: {CountyCode}\n" +
                $"\tCountyName: {CountyName}\n" +
                $"\tFragmentHouse: {FragmentHouse}\n" +
                $"\tFragmentPreDir: {FragmentPreDir}\n" +
                $"\tFragmentStreet: {FragmentStreet}\n" +
                $"\tFragmentSuffix: {FragmentSuffix}\n" +
                $"\tFragmentPostDir: {FragmentPostDir}\n" +
                $"\tFragmentUnit: {FragmentUnit}\n" +
                $"\tFragment: {Fragment}\n" +
                $"\tFragmentPMBPrefix: {FragmentPMBPrefix}\n" +
                $"\tFragmentPMBNumber: {FragmentPMBNumber}}}\n";
        }
    }

    public class CityStateZip
    {
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "State")]
        public string State { get; set; }
        [DataMember(Name = "Zip")]
        public string Zip { get; set; }
        [DataMember(Name = "GeneralDeliveryService")]
        public string GeneralDeliveryService { get; set; }
        [DataMember(Name = "POBoxService")]
        public string POBoxService { get; set; }
        [DataMember(Name = "StreetService")]
        public string StreetService { get; set; }
        [DataMember(Name = "RRHCService")]
        public string RRHCService { get; set; }
        [DataMember(Name = "UrbanizationService")]
        public string UrbanizationService { get; set; }
        [DataMember(Name = "POBoxRangeLow")]
        public string POBoxRangeLow { get; set; }
        [DataMember(Name = "POBoxRangeHigh")]
        public string POBoxRangeHigh { get; set; }
        [DataMember(Name = "IsUniqueZipCode")]
        public string IsUniqueZipCode { get; set; }
        public override string ToString()
        {
            return $"City: {City} " +
                $"State: {State} " +
                $"Zip: {Zip} " +
                $"GeneralDeliveryService: {GeneralDeliveryService} " +
                $"POBoxService: {POBoxService} " +
                $"StreetService: {StreetService} " +
                $"RRHCService: {RRHCService} " +
                $"UrbanizationService: {UrbanizationService} " +
                $"POBoxRangeLow: {POBoxRangeLow} " +
                $"POBoxRangeHigh: {POBoxRangeHigh} " +
                $"IsUniqueZipCode: {IsUniqueZipCode} ";
        }
    }

    public class Error
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }
        [DataMember(Name = "TypeCode")]
        public string TypeCode { get; set; }
        [DataMember(Name = "Desc")]
        public string Desc { get; set; }
        [DataMember(Name = "DescCode")]
        public string DescCode { get; set; }
        public override string ToString()
        {
            return $"Type: {Type} " +
                $"TypeCode: {TypeCode} " +
                $"Desc: {Desc} " +
                $"DescCode: {DescCode} ";
        }
    }
}
