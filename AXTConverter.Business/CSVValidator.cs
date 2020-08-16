using AXTConverter.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AXTConverter.Business
{
    public class CSVValidator : ClassMap<AXTViewModel>
    {
        string[] patientSex = { "M", "F" };
        char[] PRIType = { '0', '1', '2', '3', '4', '5', '6' };
        string[] pleuralPlaqueSites = { "0", "1", "2", "4", "16" , "32", "64", "256", "512", "1024", "4096", "8192", "16384" };
        string[] costophrenicAngleOblit = { "0", "1", "2", "4" };
        string[] pleuralThickeningSites = { "0", "1", "2", "4", "16", "32", "64" };
        string[] anyOtherAbnormalities = { "0", "1", "2" };
        string[] otherSymbols = { "0", "1", "2", "4", "16", "32", "64", "256", "512", "1024", "4096", "8192", "16384",
            "32768", "65536", "131072","262144", "524288", "1048576", "2097152", "4194304", "8388608","16777216","33554432","67108864","134217728" };

        string[] physicianNotificationStatus = { "0", "262144", "524288" };

        string[] parenchymalAbnormalities = { "0", "1", "2", "4", "16", "32", "64", "256", "512", "1024", "4096", "8192", "16384",
            "32768", "65536", "131072","262144", "524288", "1048576", "2097152", "4194304" };
        public CSVValidator()
        {
            AutoMap(CultureInfo.InvariantCulture);
            //Optional fields
            //Map(m => m.DatePhysicianContacted).Optional();
            //Map(m => m.WorkerSSN).Optional();
            Map(m => m.PrintedSignatureName).Optional();
            Map(m => m.ClientStreetAddress).Optional();
            Map(m => m.ClientName).Optional();
            Map(m => m.ClientCity).Optional();
            Map(m => m.ClientState).Optional();
            Map(m => m.ClientZipCode).Optional();
            Map(m => m.ClientPhone).Optional();
            Map(m => m.ClientOtherContactInfo).Optional();

            Map(m => m.PatientLastName).Validate(field => (field.Length >= 1 && field.Length < 30));
            Map(m => m.PatientFirstName).Validate(field => (field.Length >= 1 && field.Length < 30));
            Map(m => m.PatientID).Validate(field => (field.Length >= 4 && field.Length < 15));
            Map(m => m.PatientsSex).Validate(field => (patientSex.Any(s => field.Contains(s))));
            Map(m => m.PatientsBirthDate);
            Map(m => m.PatientComments);
            Map(m => m.StudyDate);
            Map(m => m.StudyTime);
            Map(m => m.AccessionNumber);
            Map(m => m.InstitutionName);
            Map(m => m.ReferringPhysiciansName);
            Map(m => m.StudyID);
            Map(m => m.StudyDescription);
            Map(m => m.Modality);
            Map(m => m.SeriesNumber);
            Map(m => m.Laterality);
            Map(m => m.SeriesDate);
            Map(m => m.SeriesTime);
            Map(m => m.ProtocolName);
            Map(m => m.SeriesDescription);
            Map(m => m.BodyPartExamined);
            Map(m => m.PatientPosition);
            Map(m => m.PatientOrientation);
            Map(m => m.InstanceNumber);
            Map(m => m.ContentDate);
            Map(m => m.ContentTime);
            Map(m => m.AcquisitionNumber);
            Map(m => m.ImageType);
            Map(m => m.InstanceCreationDate);
            Map(m => m.InstanceCreationTime);
            Map(m => m.AcquisitionDate);
            Map(m => m.AcquisitionTime);
            Map(m => m.SamplesPerPixel);
            Map(m => m.PhotometricInterpretation);
            Map(m => m.Rows);
            Map(m => m.Columns);
            Map(m => m.PixelAspectRatio);
            Map(m => m.BitsAllocated);
            Map(m => m.BitsStored);
            Map(m => m.HighBit);
            Map(m => m.PixelRepresentation);
            Map(m => m.WindowCenter);
            Map(m => m.WindowWidth);
            Map(m => m.StudyInstanceUID);
            Map(m => m.SeriesInstanceUID);
            Map(m => m.SOPInstanceUID);
            Map(m => m.ReaderLastName);
//            Map(m => m.PrintedSignatureName);
            Map(m => m.IDNumber);
            Map(m => m.Initials);
            Map(m => m.StreetAddress);
            Map(m => m.City);
            Map(m => m.State);
            Map(m => m.ZipCode);
            Map(m => m.AE_TITLE);
           // Map(m => m.ClientName);
            //Map(m => m.ClientStreetAddress);
            //Map(m => m.ClientCity);
            //Map(m => m.ClientState);
            //Map(m => m.ClientZipCode);
            //Map(m => m.ClientPhone);
            //Map(m => m.ClientOtherContactInfo);
            Map(m => m.DateOfRadiograph);
            Map(m => m.DateOfReading);
            Map(m => m.TypeOfReading);
            Map(m => m.FacilityIDNumber);
            Map(m => m.PhysicianNotificationStatus).Validate(field => (physicianNotificationStatus.Any(s => (Convert.ToUInt32(field, 2)).ToString().Contains(s))));
            Map(m => m.ImageQuality);
            Map(m => m.ImageDefectOther);
            Map(m => m.AnyParenchymalAbnorm);
            Map(m => m.ParenchymalAbnormalities).Validate( field => parenchymalAbnormalities.Any(s=> (Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.AnyPleuralAbnormalities).Validate(field => anyOtherAbnormalities.Any(s => (Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.PleuralPlaqueSites).Validate(field => (pleuralPlaqueSites.Any(s=> Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.PleuralCalcificationSites).Validate(field => (pleuralPlaqueSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.PlaqueExtent).Validate(field => (pleuralPlaqueSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s)))); 
            Map(m => m.PlaqueWidth).Validate(field => (pleuralPlaqueSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s)))); 
            Map(m => m.CostophrenicAngleOblit).Validate(field => (costophrenicAngleOblit.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.PleuralThickeningSites).Validate(field => (pleuralThickeningSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.ThickenCalcificationSites).Validate(field => (pleuralThickeningSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s)))); 
            Map(m => m.ThickeningExtent).Validate(field => (pleuralPlaqueSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.ThickeningWidth).Validate(field => (pleuralPlaqueSites.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));
            Map(m => m.AnyOtherAbnormalities).Validate(field => (anyOtherAbnormalities.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s)))); 
            Map(m => m.OtherSymbols).Validate(field => (otherSymbols.Any(s => Convert.ToUInt32(field, 2).ToString().Contains(s))));

        }
    }
}
