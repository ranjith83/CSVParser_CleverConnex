using AXTConverter.Data;
using AXTConverter.Interface;
using AXTConverter.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace AXTConverter.Business
{
    public class CSVFileParser : ICSVFileParser
    {
        public IAXTDataAccess _aXTDataAccess;
        public CSVFileParser(DatabaseContext databaseContext, IAXTDataAccess aXTDataAccess)
        {
            _aXTDataAccess = aXTDataAccess;
        }

        public AXTDataAccess AXTDataAccess { get; }

        public void TransformCSV(string filePath)
        {
            var reader = new StreamReader(filePath);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap(new CSVValidator());
            csv.Configuration.HeaderValidated = null;
            var AXTCSVRecords = csv.GetRecords<AXTViewModel>();
            var AXTModelRecords = ValidateAXT(AXTCSVRecords);
            _aXTDataAccess.BulkInsertAXTData(AXTModelRecords);
        }

        private List<AXTModel> ValidateAXT(IEnumerable<AXTViewModel> aXTViewModels)
        {
            var aXTModel = new AXTModel();
            List<AXTModel> axtModels = new List<AXTModel>();

            foreach (var axtRecord in aXTViewModels)
            {
                aXTModel.FACILITY_NBR = axtRecord.FacilityIDNumber;
               //aXTModel.WORKER_SSN = axtRecord.WorkerSSN;
                aXTModel.ClientName = axtRecord.ClientName;
                aXTModel.ClientStreetAddress = axtRecord.ClientStreetAddress;
                aXTModel.ClientCity = axtRecord.ClientCity;
                aXTModel.ClientState = axtRecord.ClientState;
                aXTModel.ClientZipCode = axtRecord.ClientZipCode;
                aXTModel.ClientPhone = axtRecord.ClientPhone;
                aXTModel.ClientOtherContactInfo = axtRecord.ClientOtherContactInfo;
                aXTModel.ReaderLastName = axtRecord.ReaderLastName;
                aXTModel.PrintedSignatureName = axtRecord.PrintedSignatureName;
                aXTModel.ReaderIDNumber = axtRecord.IDNumber;
                aXTModel.ReaderInitials = axtRecord.Initials;
                aXTModel.ReaderStreetAddress = axtRecord.StreetAddress;
                aXTModel.ReaderCity = axtRecord.City;
                aXTModel.ReaderState = axtRecord.State;
                aXTModel.ReaderZipCode = axtRecord.ZipCode;
                var tempDate = new DateTime();
                aXTModel.XRAY_DATE = Convert.ToString(axtRecord.DateOfRadiograph);
                if (DateTime.TryParse(axtRecord.DateOfReading, out tempDate))
                    aXTModel.READ_DATE = Convert.ToString(Convert.ToDateTime(axtRecord.DateOfReading));
                //if (DateTime.TryParse(axtRecord.DatePhysicianContacted, out tempDate))
                //    aXTModel.DATE_DR_NOTIFIED = Convert.ToString(Convert.ToDateTime(axtRecord.DatePhysicianContacted));
                int uint16_1 = (int)Convert.ToUInt16(axtRecord.TypeOfReading, 2);
                if ((uint16_1 & 1) > 0)
                    aXTModel.READ_TYPE = "A";
                if ((uint16_1 & 2) > 0)
                    aXTModel.READ_TYPE = "B";
                if ((uint16_1 & 4) > 0)
                    aXTModel.READ_TYPE = "P";
                int uint16_2 = (int)Convert.ToUInt16(axtRecord.ImageQuality, 2);
                if ((uint16_2 & 1) > 0)
                    aXTModel.FILM_QUALITY = "1";
                if ((uint16_2 & 2) > 0)
                    aXTModel.FILM_QUALITY = "2";
                if ((uint16_2 & 4) > 0)
                    aXTModel.FILM_QUALITY = "3";
                if ((uint16_2 & 8) > 0)
                    aXTModel.FILM_QUALITY = "4";
                if ((uint16_2 & 16) > 0)
                    aXTModel.UNREAD_OVEREXPOSED = "Y";
                if ((uint16_2 & 32) > 0)
                    aXTModel.UNREAD_UNDEREXPOSED = "Y";
                if ((uint16_2 & 64) > 0)
                    aXTModel.UNREAD_ARTIFACTS = "Y";
                if ((uint16_2 & 128) > 0)
                    aXTModel.UNREAD_POSITION = "Y";
                if ((uint16_2 & 256) > 0)
                    aXTModel.UNREAD_CONTRAST = "Y";
                if ((uint16_2 & 512) > 0)
                    aXTModel.UNREAD_PROCESSING = "Y";
                if ((uint16_2 & 1024) > 0)
                    aXTModel.UNREAD_UNDERINFLATION = "Y";
                if ((uint16_2 & 2048) > 0)
                    aXTModel.UNREAD_MOTTLE = "Y";
                if ((uint16_2 & 4096) > 0)
                    aXTModel.UNREAD_OTHER = "Y";
                if ((uint16_2 & 8192) > 0)
                    aXTModel.UNREAD_EDGE = "Y";
                aXTModel.UNREAD_OTHER_COMMENT = axtRecord.ImageDefectOther;
                int uint32_1 = (int)checked((ushort)Convert.ToUInt32(axtRecord.AnyParenchymalAbnorm, 2));
                if (uint32_1 == 1)
                    aXTModel.Q2A = "Y";
                if (uint32_1 == 0)
                    aXTModel.Q2A = "N";
                if (uint32_1 == 2)
                    aXTModel.Q2A = "";
                int uint32_2 = (int)Convert.ToUInt32(axtRecord.ParenchymalAbnormalities, 2);
                if ((uint32_2 & 1) == 1)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "P";
                if ((uint32_2 & 2) == 2)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "Q";
                if ((uint32_2 & 3) == 3)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "R";
                if ((uint32_2 & 4) == 4)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "S";
                if ((uint32_2 & 5) == 5)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "T";
                if ((uint32_2 & 6) == 6)
                    aXTModel.SMALL_OPAC_PRI_TYPE = "U";
                if ((uint32_2 & 16) == 16)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "P";
                if ((uint32_2 & 32) == 32)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "Q";
                if ((uint32_2 & 48) == 48)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "R";
                if ((uint32_2 & 64) == 64)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "S";
                if ((uint32_2 & 80) == 80)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "T";
                if ((uint32_2 & 96) == 96)
                    aXTModel.SMALL_OPAC_SEC_TYPE = "U";
                if ((uint)(uint32_2 & 256) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_UR = "1";
                if ((uint)(uint32_2 & 1024) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_MR = "2";
                if ((uint)(uint32_2 & 4096) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_LR = "3";
                if ((uint)(uint32_2 & 512) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_UL = "4";
                if ((uint)(uint32_2 & 2048) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_ML = "5";
                if ((uint)(uint32_2 & 8192) > 0U)
                    aXTModel.SMALL_OPAC_ZONE_LL = "6";
                if ((uint32_2 & 65536) == 65536)
                    aXTModel.SMALL_OPAC_CATEGORY = "0-";
                if ((uint32_2 & 131072) == 131072)
                    aXTModel.SMALL_OPAC_CATEGORY = "00";
                if ((uint32_2 & 196608) == 196608)
                    aXTModel.SMALL_OPAC_CATEGORY = "01";
                if ((uint32_2 & 262144) == 262144)
                    aXTModel.SMALL_OPAC_CATEGORY = "10";
                if ((uint32_2 & 327680) == 327680)
                    aXTModel.SMALL_OPAC_CATEGORY = "11";
                if ((uint32_2 & 393216) == 393216)
                    aXTModel.SMALL_OPAC_CATEGORY = "12";
                if ((uint32_2 & 458752) == 458752)
                    aXTModel.SMALL_OPAC_CATEGORY = "21";
                if ((uint32_2 & 524288) == 524288)
                    aXTModel.SMALL_OPAC_CATEGORY = "22";
                if ((uint32_2 & 589824) == 589824)
                    aXTModel.SMALL_OPAC_CATEGORY = "23";
                if ((uint32_2 & 655360) == 655360)
                    aXTModel.SMALL_OPAC_CATEGORY = "32";
                if ((uint32_2 & 720896) == 720896)
                    aXTModel.SMALL_OPAC_CATEGORY = "33";
                if ((uint32_2 & 786432) == 786432)
                    aXTModel.SMALL_OPAC_CATEGORY = "3+";
                if ((uint32_2 & 1048576) == 1048576)
                    aXTModel.LARGE_OPAC_STAGE = "O";
                if ((uint32_2 & 2097152) == 2097152)
                    aXTModel.LARGE_OPAC_STAGE = "A";
                if ((uint32_2 & 3145728) == 3145728)
                    aXTModel.LARGE_OPAC_STAGE = "B";
                if ((uint32_2 & 4194304) == 4194304)
                    aXTModel.LARGE_OPAC_STAGE = "C";
                int uint16_3 = (int)Convert.ToUInt16(axtRecord.AnyPleuralAbnormalities, 2);
                if (uint16_3 == 1)
                    aXTModel.Q3A = "Y";
                if (uint16_3 == 0)
                    aXTModel.Q3A = "N";
                if (uint16_3 == 2)
                    aXTModel.Q3A = "";
                ushort uint16_4 = Convert.ToUInt16(axtRecord.PleuralPlaqueSites, 2);
                if (((int)uint16_4 & 1) > 0)
                    aXTModel.PLEURAL_SITE_PROFILE = "O";
                if (((int)uint16_4 & 2) > 0)
                    aXTModel.PLEURAL_SITE_PROFILE = "R";
                if (((int)uint16_4 & 4) > 0)
                    aXTModel.PLEURAL_SITE_PROFILE = "L";
                if (((int)uint16_4 & 4) > 0 & ((int)uint16_4 & 2) > 0)
                    aXTModel.PLEURAL_SITE_PROFILE = "RL";
                if (((int)uint16_4 & 16) > 0)
                    aXTModel.PLEURAL_SITE_FACE = "O";
                if (((int)uint16_4 & 32) > 0)
                    aXTModel.PLEURAL_SITE_FACE = "R";
                if (((int)uint16_4 & 64) > 0)
                    aXTModel.PLEURAL_SITE_FACE = "L";
                if (((int)uint16_4 & 64) > 0 & ((int)uint16_4 & 32) > 0)
                    aXTModel.PLEURAL_SITE_FACE = "RL";
                if (((int)uint16_4 & 256) > 0)
                    aXTModel.PLEURAL_SITE_DIAPHRAGM = "O";
                if (((int)uint16_4 & 512) > 0)
                    aXTModel.PLEURAL_SITE_DIAPHRAGM = "R";
                if (((int)uint16_4 & 1024) > 0)
                    aXTModel.PLEURAL_SITE_DIAPHRAGM = "L";
                if (((int)uint16_4 & 1024) > 0 & ((int)uint16_4 & 512) > 0)
                    aXTModel.PLEURAL_SITE_DIAPHRAGM = "RL";
                if (((int)uint16_4 & 4096) > 0)
                    aXTModel.PLEURAL_SITE_OTHER = "O";
                if (((int)uint16_4 & 8192) > 0)
                    aXTModel.PLEURAL_SITE_OTHER = "R";
                if (((int)uint16_4 & 16384) > 0)
                    aXTModel.PLEURAL_SITE_OTHER = "L";
                if (((int)uint16_4 & 16384) > 0 & ((int)uint16_4 & 8192) > 0)
                    aXTModel.PLEURAL_SITE_OTHER = "RL";
                ushort uint16_5 = Convert.ToUInt16(axtRecord.PleuralCalcificationSites, 2);
                if (((int)uint16_5 & 1) > 0)
                    aXTModel.PLEURAL_CALC_PROFILE = "O";
                if (((int)uint16_5 & 2) > 0)
                    aXTModel.PLEURAL_CALC_PROFILE = "R";
                if (((int)uint16_5 & 4) > 0)
                    aXTModel.PLEURAL_CALC_PROFILE = "L";
                if (((int)uint16_5 & 4) > 0 & ((int)uint16_5 & 2) > 0)
                    aXTModel.PLEURAL_CALC_PROFILE = "RL";
                if (((int)uint16_5 & 16) > 0)
                    aXTModel.PLEURAL_CALC_FACE = "O";
                if (((int)uint16_5 & 32) > 0)
                    aXTModel.PLEURAL_CALC_FACE = "R";
                if (((int)uint16_5 & 64) > 0)
                    aXTModel.PLEURAL_CALC_FACE = "L";
                if (((int)uint16_5 & 64) > 0 & ((int)uint16_5 & 32) > 0)
                    aXTModel.PLEURAL_CALC_FACE = "RL";
                if (((int)uint16_5 & 256) > 0)
                    aXTModel.PLEURAL_CALC_DIAPHRAGM = "O";
                if (((int)uint16_5 & 512) > 0)
                    aXTModel.PLEURAL_CALC_DIAPHRAGM = "R";
                if (((int)uint16_5 & 1024) > 0)
                    aXTModel.PLEURAL_CALC_DIAPHRAGM = "L";
                if (((int)uint16_5 & 1024) > 0 & ((int)uint16_5 & 512) > 0)
                    aXTModel.PLEURAL_CALC_DIAPHRAGM = "RL";
                if (((int)uint16_5 & 4096) > 0)
                    aXTModel.PLEURAL_CALC_OTHER = "O";
                if (((int)uint16_5 & 8192) > 0)
                    aXTModel.PLEURAL_CALC_OTHER = "R";
                if (((int)uint16_5 & 16384) > 0)
                    aXTModel.PLEURAL_CALC_OTHER = "L";
                if (((int)uint16_5 & 16384) > 0 & ((int)uint16_5 & 8192) > 0)
                    aXTModel.PLEURAL_CALC_OTHER = "RL";
                int uint16_6 = (int)Convert.ToUInt16(axtRecord.PlaqueExtent, 2);
                if ((uint16_6 & 1) > 0)
                    aXTModel.PLEURAL_EXTENT_R = "O";
                if ((uint16_6 & 2) > 0)
                    aXTModel.PLEURAL_EXTENT_R = "R";
                if ((uint16_6 & 16) > 0)
                    aXTModel.PLEURAL_EXTENT_R_123 = "1";
                if ((uint16_6 & 32) > 0)
                    aXTModel.PLEURAL_EXTENT_R_123 = "2";
                if ((uint16_6 & 64) > 0)
                    aXTModel.PLEURAL_EXTENT_R_123 = "3";
                if ((uint16_6 & 256) > 0)
                    aXTModel.PLEURAL_EXTENT_L = "O";
                if ((uint16_6 & 512) > 0)
                    aXTModel.PLEURAL_EXTENT_L = "L";
                if ((uint16_6 & 4096) > 0)
                    aXTModel.PLEURAL_EXTENT_L_123 = "1";
                if ((uint16_6 & 8192) > 0)
                    aXTModel.PLEURAL_EXTENT_L_123 = "2";
                if ((uint16_6 & 16384) > 0)
                    aXTModel.PLEURAL_EXTENT_L_123 = "3";
                int uint16_7 = (int)Convert.ToUInt16(axtRecord.PlaqueWidth, 2);
                if ((uint16_7 & 1) > 0)
                    aXTModel.PLEURAL_WIDTH_R = "O";
                if ((uint16_7 & 2) > 0)
                    aXTModel.PLEURAL_WIDTH_R = "R";
                if ((uint16_7 & 16) > 0)
                    aXTModel.PLEURAL_WIDTH_R_ABC = "A";
                if ((uint16_7 & 32) > 0)
                    aXTModel.PLEURAL_WIDTH_R_ABC = "B";
                if ((uint16_7 & 64) > 0)
                    aXTModel.PLEURAL_WIDTH_R_ABC = "C";
                if ((uint16_7 & 256) > 0)
                    aXTModel.PLEURAL_WIDTH_L = "O";
                if ((uint16_7 & 512) > 0)
                    aXTModel.PLEURAL_WIDTH_L = "L";
                if ((uint16_7 & 4096) > 0)
                    aXTModel.PLEURAL_WIDTH_L_ABC = "A";
                if ((uint16_7 & 8192) > 0)
                    aXTModel.PLEURAL_WIDTH_L_ABC = "B";
                if ((uint16_7 & 16384) > 0)
                    aXTModel.PLEURAL_WIDTH_L_ABC = "C";
                ushort uint16_8 = Convert.ToUInt16(axtRecord.CostophrenicAngleOblit, 2);
                if (((int)uint16_8 & 1) > 0)
                    aXTModel.Q3C = "N";
                if (((int)uint16_8 & 2) > 0)
                    aXTModel.Q3C = "R";
                if (((int)uint16_8 & 4) > 0)
                    aXTModel.Q3C = "L";
                if (((int)uint16_8 & 2) > 0 & ((int)uint16_8 & 4) > 0)
                    aXTModel.Q3C = "RL";
                ushort uint16_9 = Convert.ToUInt16(axtRecord.PleuralThickeningSites, 2);
                if (((int)uint16_9 & 1) > 0)
                    aXTModel.DIFFUSE_SITE_PROFILE = "O";
                if (((int)uint16_9 & 2) > 0)
                    aXTModel.DIFFUSE_SITE_PROFILE = "R";
                if (((int)uint16_9 & 4) > 0)
                    aXTModel.DIFFUSE_SITE_PROFILE = "L";
                if (((int)uint16_9 & 4) > 0 & ((int)uint16_9 & 2) > 0)
                    aXTModel.DIFFUSE_SITE_PROFILE = "RL";
                if (((int)uint16_9 & 16) > 0)
                    aXTModel.DIFFUSE_SITE_FACE = "O";
                if (((int)uint16_9 & 32) > 0)
                    aXTModel.DIFFUSE_SITE_FACE = "R";
                if (((int)uint16_9 & 64) > 0)
                    aXTModel.DIFFUSE_SITE_FACE = "L";
                if (((int)uint16_9 & 64) > 0 & ((int)uint16_9 & 32) > 0)
                    aXTModel.DIFFUSE_SITE_FACE = "RL";
                ushort uint16_10 = Convert.ToUInt16(axtRecord.ThickenCalcificationSites, 2);
                if (((int)uint16_10 & 1) > 0)
                    aXTModel.DIFFUSE_CALC_PROFILE = "O";
                if (((int)uint16_10 & 2) > 0)
                    aXTModel.DIFFUSE_CALC_PROFILE = "R";
                if (((int)uint16_10 & 4) > 0)
                    aXTModel.DIFFUSE_CALC_PROFILE = "L";
                if (((int)uint16_10 & 4) > 0 & ((int)uint16_10 & 2) > 0)
                    aXTModel.DIFFUSE_CALC_PROFILE = "RL";
                if (((int)uint16_10 & 16) > 0)
                    aXTModel.DIFFUSE_CALC_FACE = "O";
                if (((int)uint16_10 & 32) > 0)
                    aXTModel.DIFFUSE_CALC_FACE = "R";
                if (((int)uint16_10 & 64) > 0)
                    aXTModel.DIFFUSE_CALC_FACE = "L";
                if (((int)uint16_10 & 64) > 0 & ((int)uint16_10 & 32) > 0)
                    aXTModel.DIFFUSE_CALC_FACE = "RL";
                int uint16_11 = (int)Convert.ToUInt16(axtRecord.ThickeningExtent, 2);
                if ((uint16_11 & 1) > 0)
                    aXTModel.DIFFUSE_EXTENT_R = "O";
                if ((uint16_11 & 2) > 0)
                    aXTModel.DIFFUSE_EXTENT_R = "R";
                if ((uint16_11 & 16) > 0)
                    aXTModel.DIFFUSE_EXTENT_R_123 = "1";
                if ((uint16_11 & 32) > 0)
                    aXTModel.DIFFUSE_EXTENT_R_123 = "2";
                if ((uint16_11 & 64) > 0)
                    aXTModel.DIFFUSE_EXTENT_R_123 = "3";
                if ((uint16_11 & 256) > 0)
                    aXTModel.DIFFUSE_EXTENT_L = "O";
                if ((uint16_11 & 512) > 0)
                    aXTModel.DIFFUSE_EXTENT_L = "L";
                if ((uint16_11 & 4096) > 0)
                    aXTModel.DIFFUSE_EXTENT_L_123 = "1";
                if ((uint16_11 & 8192) > 0)
                    aXTModel.DIFFUSE_EXTENT_L_123 = "2";
                if ((uint16_11 & 16384) > 0)
                    aXTModel.DIFFUSE_EXTENT_L_123 = "3";
                int uint16_12 = (int)Convert.ToUInt16(axtRecord.ThickeningWidth, 2);
                if ((uint16_12 & 1) > 0)
                    aXTModel.DIFFUSE_WIDTH_R = "O";
                if ((uint16_12 & 2) > 0)
                    aXTModel.DIFFUSE_WIDTH_R = "R";
                if ((uint16_12 & 16) > 0)
                    aXTModel.DIFFUSE_WIDTH_R_ABC = "A";
                if ((uint16_12 & 32) > 0)
                    aXTModel.DIFFUSE_WIDTH_R_ABC = "B";
                if ((uint16_12 & 64) > 0)
                    aXTModel.DIFFUSE_WIDTH_R_ABC = "C";
                if ((uint16_12 & 256) > 0)
                    aXTModel.DIFFUSE_WIDTH_L = "O";
                if ((uint16_12 & 512) > 0)
                    aXTModel.DIFFUSE_WIDTH_L = "L";
                if ((uint16_12 & 4096) > 0)
                    aXTModel.DIFFUSE_WIDTH_L_ABC = "A";
                if ((uint16_12 & 8192) > 0)
                    aXTModel.DIFFUSE_WIDTH_L_ABC = "B";
                if ((uint16_12 & 16384) > 0)
                    aXTModel.DIFFUSE_WIDTH_L_ABC = "C";
                int uint16_13 = (int)Convert.ToUInt16(axtRecord.AnyOtherAbnormalities, 2);
                if (uint16_13 == 1)
                    aXTModel.Q4A = "Y";
                if (uint16_13 == 0)
                    aXTModel.Q4A = "N";
                if (uint16_13 == 2)
                    aXTModel.Q4A = "";
                int uint32_3 = (int)Convert.ToUInt32(axtRecord.OtherSymbols, 2);
                if ((uint)(uint32_3 & 1) > 0U)
                    aXTModel.OTHER_SYMBOLS_AA = "Y";
                if ((uint)(uint32_3 & 2) > 0U)
                    aXTModel.OTHER_SYMBOLS_AT = "Y";
                if ((uint)(uint32_3 & 4) > 0U)
                    aXTModel.OTHER_SYMBOLS_AX = "Y";
                if ((uint)(uint32_3 & 8) > 0U)
                    aXTModel.OTHER_SYMBOLS_BU = "Y";
                if ((uint)(uint32_3 & 16) > 0U)
                    aXTModel.OTHER_SYMBOLS_CA = "Y";
                if ((uint)(uint32_3 & 32) > 0U)
                    aXTModel.OTHER_SYMBOLS_CG = "Y";
                if ((uint)(uint32_3 & 64) > 0U)
                    aXTModel.OTHER_SYMBOLS_CN = "Y";
                if ((uint)(uint32_3 & 128) > 0U)
                    aXTModel.OTHER_SYMBOLS_CO = "Y";
                if ((uint)(uint32_3 & 256) > 0U)
                    aXTModel.OTHER_SYMBOLS_CP = "Y";
                if ((uint)(uint32_3 & 512) > 0U)
                    aXTModel.OTHER_SYMBOLS_CV = "Y";
                if ((uint)(uint32_3 & 1024) > 0U)
                    aXTModel.OTHER_SYMBOLS_DI = "Y";
                if ((uint)(uint32_3 & 2048) > 0U)
                    aXTModel.OTHER_SYMBOLS_EF = "Y";
                if ((uint)(uint32_3 & 4096) > 0U)
                    aXTModel.OTHER_SYMBOLS_EM = "Y";
                if ((uint)(uint32_3 & 8192) > 0U)
                    aXTModel.OTHER_SYMBOLS_ES = "Y";
                if ((uint)(uint32_3 & 16384) > 0U)
                    aXTModel.OTHER_SYMBOLS_FR = "Y";
                if ((uint)(uint32_3 & 32768) > 0U)
                    aXTModel.OTHER_SYMBOLS_HI = "Y";
                if ((uint)(uint32_3 & 65536) > 0U)
                    aXTModel.OTHER_SYMBOLS_HO = "Y";
                if ((uint)(uint32_3 & 131072) > 0U)
                    aXTModel.OTHER_SYMBOLS_ID = "Y";
                if ((uint)(uint32_3 & 262144) > 0U)
                    aXTModel.OTHER_SYMBOLS_IH = "Y";
                if ((uint)(uint32_3 & 524288) > 0U)
                    aXTModel.OTHER_SYMBOLS_KL = "Y";
                if ((uint)(uint32_3 & 1048576) > 0U)
                    aXTModel.OTHER_SYMBOLS_ME = "Y";
                if ((uint)(uint32_3 & 2097152) > 0U)
                    aXTModel.OTHER_SYMBOLS_PA = "Y";
                if ((uint)(uint32_3 & 4194304) > 0U)
                    aXTModel.OTHER_SYMBOLS_PB = "Y";
                if ((uint)(uint32_3 & 8388608) > 0U)
                    aXTModel.OTHER_SYMBOLS_PI = "Y";
                if ((uint)(uint32_3 & 16777216) > 0U)
                    aXTModel.OTHER_SYMBOLS_PX = "Y";
                if ((uint)(uint32_3 & 33554432) > 0U)
                    aXTModel.OTHER_SYMBOLS_RA = "Y";
                if ((uint)(uint32_3 & 67108864) > 0U)
                    aXTModel.OTHER_SYMBOLS_RP = "Y";
                if ((uint)(uint32_3 & 134217728) > 0U)
                    aXTModel.OTHER_SYMBOLS_TB = "Y";
                if (axtRecord.PhysicianNotificationStatus == null)
                {
                    aXTModel.SEE_DR = "";
                }
                else
                {
                    int uint32_4 = (int)Convert.ToUInt32(axtRecord.PhysicianNotificationStatus, 2);
                    aXTModel.SEE_DR = "";
                    if (((uint)uint32_4 & 262144) > 0)
                        aXTModel.SEE_DR = "Y";
                    if (((uint)uint32_4 & 524288) > 0)
                        aXTModel.SEE_DR = "N";
                }
                int uint32_5 = (int)Convert.ToUInt32(axtRecord.OtherAbnormalities, 2);
                if ((uint)(uint32_5 & 1) > 0U)
                    aXTModel.OTHER_DISEASES_EVENTRATION = "Y";
                if ((uint)(uint32_5 & 2) > 0U)
                    aXTModel.OTHER_DISEASES_HIATAL_HERNIA = "Y";
                if ((uint)(uint32_5 & 4) > 0U)
                    aXTModel.OTHER_DISEASES_BRONCHOVASCULAR = "Y";
                if ((uint)(uint32_5 & 8) > 0U)
                    aXTModel.OTHER_DISEASES_HYPERINFLATION = "Y";
                if ((uint)(uint32_5 & 16) > 0U)
                    aXTModel.OTHER_DISEASES_BONY_CHEST_CAGE = "Y";
                if ((uint)(uint32_5 & 32) > 0U)
                    aXTModel.OTHER_DISEASES_FRACTURE_HEALED = "Y";
                if ((uint)(uint32_5 & 64) > 0U)
                    aXTModel.OTHER_DISEASES_FRACTURE_NONHEALED = "Y";
                if ((uint)(uint32_5 & 128) > 0U)
                    aXTModel.OTHER_DISEASES_SCOLIOSIS = "Y";
                if ((uint)(uint32_5 & 256) > 0U)
                    aXTModel.OTHER_DISEASES_VERTEBRAL_COLUMN = "Y";
                if ((uint)(uint32_5 & 512) > 0U)
                    aXTModel.OTHER_DISEASES_AZYGOS_LOBE = "Y";
                if ((uint)(uint32_5 & 1024) > 0U)
                    aXTModel.OTHER_DISEASES_LUNG_DENSITY = "Y";
                if ((uint)(uint32_5 & 2048) > 0U)
                    aXTModel.OTHER_DISEASES_INFILTRATE = "Y";
                if ((uint)(uint32_5 & 4096) > 0U)
                    aXTModel.OTHER_DISEASES_NODULAR_LESION = "Y";
                if ((uint)(uint32_5 & 8192) > 0U)
                    aXTModel.OTHER_DISEASES_FOREIGN_BODY = "Y";
                if ((uint)(uint32_5 & 16384) > 0U)
                    aXTModel.OTHER_DISEASES_POST_SURGICAL = "Y";
                if ((uint)(uint32_5 & 32768) > 0U)
                    aXTModel.OTHER_DISEASES_CYST = "Y";
                if ((uint)(uint32_5 & 65536) > 0U)
                    aXTModel.OTHER_DISEASES_AORTA_ANOMALY = "Y";
                if ((uint)(uint32_5 & 131072) > 0U)
                    aXTModel.OTHER_DISEASES_VASCULAR = "Y";
                if (((long)(uint)uint32_5 | 0L) > 0L)
                    aXTModel.OTHER_DISEASES = "Y";
                aXTModel.OTHER_COMMENTS = axtRecord.OtherAbnormalitiesComments.Trim();
                aXTModel.PatientLastName = axtRecord.PatientLastName.Trim();
                aXTModel.PatientFirstName = axtRecord.PatientFirstName.Trim();
                aXTModel.PatientID = axtRecord.PatientID.Trim();
                aXTModel.PatientsBirthDate = axtRecord.PatientsBirthDate.Trim();
                aXTModel.PatientsSex = axtRecord.PatientsSex.Trim();
                aXTModel.PatientComments = axtRecord.PatientComments.Trim();
                aXTModel.StudyDate = axtRecord.StudyDate.Trim();
                aXTModel.StudyTime = axtRecord.StudyTime.Trim();
                aXTModel.AccessionNumber = axtRecord.AccessionNumber.Trim();
                aXTModel.ReferringPhysiciansName = axtRecord.ReferringPhysiciansName.Trim();
                aXTModel.InstitutionName = axtRecord.InstitutionName.Trim();
                aXTModel.StudyID = axtRecord.StudyID.Trim();
                aXTModel.StudyDescription = axtRecord.StudyDescription.Trim();
                aXTModel.Modality = axtRecord.Modality.Trim();
                aXTModel.SeriesNumber = axtRecord.SeriesNumber.Trim();
                aXTModel.Laterality = axtRecord.Laterality.Trim();
                aXTModel.SeriesDate = axtRecord.SeriesDate.Trim();
                aXTModel.SeriesTime = axtRecord.SeriesTime.Trim();
                aXTModel.ProtocolName = axtRecord.ProtocolName.Trim();
                aXTModel.SeriesDescription = axtRecord.SeriesDescription.Trim();
                aXTModel.BodyPartExamined = axtRecord.BodyPartExamined.Trim();
                aXTModel.PatientPosition = axtRecord.PatientPosition.Trim();
                aXTModel.PatientOrientation = axtRecord.PatientOrientation.Trim();
                aXTModel.InstanceNumber = axtRecord.InstanceNumber.Trim();
                aXTModel.ContentDate = axtRecord.ContentDate.Trim();
                aXTModel.ContentTime = axtRecord.ContentTime.Trim();
                aXTModel.AcquisitionNumber = axtRecord.AcquisitionNumber.Trim();
                aXTModel.ImageType = axtRecord.ImageType.Trim();
                aXTModel.InstanceCreationDate = axtRecord.InstanceCreationDate.Trim();
                aXTModel.InstanceCreationTime = axtRecord.InstanceCreationTime.Trim();
                aXTModel.AcquisitionDate = axtRecord.AcquisitionDate.Trim();
                aXTModel.AcquisitionTime = axtRecord.AcquisitionTime.Trim();
                aXTModel.SamplesPerPixel = axtRecord.SamplesPerPixel.Trim();
                aXTModel.PhotometricInterpretation = axtRecord.PhotometricInterpretation.Trim();
                aXTModel.Rows = axtRecord.Rows.Trim();
                aXTModel.Columns = axtRecord.Columns.Trim();
                aXTModel.PixelAspectRatio = axtRecord.PixelAspectRatio.Trim();
                aXTModel.BitsAllocated = axtRecord.BitsAllocated.Trim();
                aXTModel.BitsStored = axtRecord.BitsStored.Trim();
                aXTModel.HighBit = axtRecord.HighBit.Trim();
                aXTModel.PixelRepresentation = axtRecord.PixelRepresentation.Trim();
                aXTModel.WindowCenter = axtRecord.WindowCenter.Trim();
                aXTModel.WindowWidth = axtRecord.WindowWidth.Trim();
                aXTModel.StudyInstanceUID = axtRecord.StudyInstanceUID.Trim();
                aXTModel.SeriesInstanceUID = axtRecord.SeriesInstanceUID.Trim();
                aXTModel.SOPInstanceUID = axtRecord.SOPInstanceUID.Trim();
                aXTModel.AETitle = axtRecord.AE_TITLE.Trim();
                aXTModel.AdjustedWindowWidth = axtRecord.AdjustedWindowWidth.Trim();
                aXTModel.AdjustedWindowCenter = axtRecord.AdjustedWindowCenter.Trim();
                aXTModel.MaxGrayscaleValue = axtRecord.MaxGrayscaleValue.Trim();
                aXTModel.AdjustedGamma = axtRecord.AdjustedGamma.Trim();
                aXTModel.TimeStudyFirstOpened = axtRecord.TimeStudyFirstOpened.Trim();
                aXTModel.TimeReportApproved = axtRecord.TimeReportApproved.Trim();
                axtModels.Add(aXTModel);
            }

            return axtModels;
        }
        

    }
}
