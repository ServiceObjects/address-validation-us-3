export class Address {
    constructor(data = {}) {
        this.Address1 = data.Address1;
        this.Address2 = data.Address2;
        this.City = data.City;
        this.State = data.State;
        this.Zip = data.Zip;
        this.IsResidential = data.IsResidential;
        this.DPV = data.DPV;
        this.DPVDesc = data.DPVDesc;
        this.DPVNotes = data.DPVNotes;
        this.DPVNotesDesc = data.DPVNotesDesc;
        this.Corrections = data.Corrections;
        this.CorrectionsDesc = data.CorrectionsDesc;
        this.BarcodeDigits = data.BarcodeDigits;
        this.CarrierRoute = data.CarrierRoute;
        this.CongressCode = data.CongressCode;
        this.CountyCode = data.CountyCode;
        this.CountyName = data.CountyName;
        this.FragmentHouse = data.FragmentHouse;
        this.FragmentPreDir = data.FragmentPreDir;
        this.FragmentStreet = data.FragmentStreet;
        this.FragmentSuffix = data.FragmentSuffix;
        this.FragmentPostDir = data.FragmentPostDir;
        this.FragmentUnit = data.FragmentUnit;
        this.Fragment = data.Fragment;
        this.FragmentPMBPrefix = data.FragmentPMBPrefix;
        this.FragmentPMBNumber = data.FragmentPMBNumber;
    }

    toString() {
        return `Address1: ${this.Address1}, ` +
            `Address2: ${this.Address2}, ` +
            `City: ${this.City}, ` +
            `State: ${this.State}, ` +
            `Zip: ${this.Zip}, ` +
            `IsResidential: ${this.IsResidential}, ` +
            `DPV: ${this.DPV}, ` +
            `DPVDesc: ${this.DPVDesc}, ` +
            `DPVNotes: ${this.DPVNotes}, ` +
            `DPVNotesDesc: ${this.DPVNotesDesc}, ` +
            `Corrections: ${this.Corrections}, ` +
            `CorrectionsDesc: ${this.CorrectionsDesc}, ` +
            `BarcodeDigits: ${this.BarcodeDigits}, ` +
            `CarrierRoute: ${this.CarrierRoute}, ` +
            `CongressCode: ${this.CongressCode}, ` +
            `CountyCode: ${this.CountyCode}, ` +
            `CountyName: ${this.CountyName}, ` +
            `FragmentHouse: ${this.FragmentHouse}, ` +
            `FragmentPreDir: ${this.FragmentPreDir}, ` +
            `FragmentStreet: ${this.FragmentStreet}, ` +
            `FragmentSuffix: ${this.FragmentSuffix}, ` +
            `FragmentPostDir: ${this.FragmentPostDir}, ` +
            `FragmentUnit: ${this.FragmentUnit}, ` +
            `Fragment: ${this.Fragment}, ` +
            `FragmentPMBPrefix: ${this.FragmentPMBPrefix}, ` +
            `FragmentPMBNumber: ${this.FragmentPMBNumber}`;
    }
}

export class Error {
    constructor(data = {}) {
        this.Type = data.Type;
        this.TypeCode = data.TypeCode;
        this.Desc = data.Desc;
        this.DescCode = data.DescCode;
    }

    toString() {
        return `Type: ${this.Type}, TypeCode: ${this.TypeCode}, Desc: ${this.Desc}, DescCode: ${this.DescCode}`;
    }
}

export class GetBestMatchesResponse {
    constructor(data = {}) {
        this.Addresses = (data.Addresses || []).map(address => new Address(address));
        this.IsCASS = data.IsCASS;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        const addressesString = this.Addresses.length
            ? this.Addresses.map(address => address.toString()).join('\n')
            : 'None';
        return `GetBestMatchesResponse:\n` +
            `Addresses: [${addressesString}], ` +
            `IsCASS: ${this.IsCASS}, ` +
            `Error: ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export class GetSecondaryNumbersResponse {
    constructor(data = {}) {
        this.Address1 = data.Address1;
        this.City = data.City;
        this.State = data.State;
        this.Zip = data.Zip;
        this.SecondaryNumbers = data.SecondaryNumbers || [];
        this.TotalCount = data.TotalCount || 0;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        const secondaryNumbersString = this.SecondaryNumbers.length
            ? this.SecondaryNumbers.join(', ')
            : 'None';
        return `GetSecondaryNumbersResponse:\n` +
            `Address1: ${this.Address1}, ` +
            `City: ${this.City}, ` +
            `State: ${this.State}, ` +
            `Zip: ${this.Zip}, ` +
            `SecondaryNumbers: [${secondaryNumbersString}], ` +
            `TotalCount: ${this.TotalCount}, ` +
            `Error: ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export class CityStateZip {
    constructor(data = {}) {
        this.City = data.City;
        this.State = data.State;
        this.Zip = data.Zip;
        this.GeneralDeliveryService = data.GeneralDeliveryService;
        this.POBoxService = data.POBoxService;
        this.StreetService = data.StreetService;
        this.RRHCService = data.RRHCService;
        this.UrbanizationService = data.UrbanizationService;
        this.POBoxRangeLow = data.POBoxRangeLow;
        this.POBoxRangeHigh = data.POBoxRangeHigh;
        this.IsUniqueZipCode = data.IsUniqueZipCode;
    }

    toString() {
        return `City: ${this.City}, ` +
            `State: ${this.State}, ` +
            `Zip: ${this.Zip}, ` +
            `GeneralDeliveryService: ${this.GeneralDeliveryService}, ` +
            `POBoxService: ${this.POBoxService}, ` +
            `StreetService: ${this.StreetService}, ` +
            `RRHCService: ${this.RRHCService}, ` +
            `UrbanizationService: ${this.UrbanizationService}, ` +
            `POBoxRangeLow: ${this.POBoxRangeLow}, ` +
            `POBoxRangeHigh: ${this.POBoxRangeHigh}, ` +
            `IsUniqueZipCode: ${this.IsUniqueZipCode}`;
    }
}

export class ValidateCityStateZipResponse {
    constructor(data = {}) {
        this.CityStateZip = data.CityStateZip ? new CityStateZip(data.CityStateZip) : null;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `ValidateCityStateZipResponse:\n` +
            `CityStateZip: ${this.CityStateZip ? this.CityStateZip.toString() : 'null'}, ` +
            `Error: ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export class GetBestMatchesSingleLineResponse {
    constructor(data = {}) {
        this.Addresses = (data.Addresses || []).map(address => new Address(address));
        this.IsCASS = data.IsCASS;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        const addressesString = this.Addresses.length
            ? this.Addresses.map(address => address.toString()).join('\n')
            : 'None';
        return `GetBestMatchesSingleLineResponse:\n` +
            `Addresses: [${addressesString}], ` +
            `IsCASS: ${this.IsCASS}, ` +
            `Error: ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export default {
    GetBestMatchesResponse,
    GetSecondaryNumbersResponse,
    ValidateCityStateZipResponse,
    GetBestMatchesSingleLineResponse
};