using System;
using System.Collections.Generic;
using System.Text;

namespace AXTConverter.Models.Enums
{
    enum ImageQuality
    {
        FILM_QUALITY1 = 1,
    FILM_QUALITY2 = 2,
    FILM_QUALITY3 = 4,
    FILM_QUALITY4 = 8,
    UNREAD_OVEREXPOSED = 16,
    UNREAD_UNDEREXPOSED = 32,
    UNREAD_ARTIFACTS = 64,
    UNREAD_POSITION = 128,
    UNREAD_CONTRAST = 256,
    UNREAD_PROCESSING = 512,
    UNREAD_UNDERINFLATION = 1024,
    UNREAD_MOTTLE = 2048,
    UNREAD_OTHER = 4096,
    UNREAD_EDGE = 8192
    }
    enum SMALL_OPAC_PRI_TYPE
    {
        SMALL_OPAC_PRI_TYPE_P = 1,
        SMALL_OPAC_PRI_TYPE_Q = 2,
        SMALL_OPAC_PRI_TYPE_R = 3,
        SMALL_OPAC_PRI_TYPE_S = 4,
        SMALL_OPAC_PRI_TYPE_T = 5,
        SMALL_OPAC_PRI_TYPE_U = 6
    }
    enum SMALL_OPAC_SEC_TYPE
    {
        SMALL_OPAC_SEC_TYPE_P = 1,
        SMALL_OPAC_SEC_TYPE_Q = 2,
        SMALL_OPAC_SEC_TYPE_R = 3,
        SMALL_OPAC_SEC_TYPE_S = 4,
        SMALL_OPAC_SEC_TYPE_T = 5,
        SMALL_OPAC_SEC_TYPE_U = 6
    }
    enum SMALL_OPAC_ZONE
    {
        SMALL_OPAC_ZONE_UR = 1,
        SMALL_OPAC_ZONE_UL = 2,
        SMALL_OPAC_ZONE_MR = 4,
        SMALL_OPAC_ZONE_ML = 8,
        SMALL_OPAC_ZONE_LR = 16,
        SMALL_OPAC_ZONE_LL = 32
    }
    enum SMALL_OPAC_CATEGORY
    {
        SMALL_OPAC_CATEGORY_0minus = 1,
        SMALL_OPAC_CATEGORY_00 = 2,
        SMALL_OPAC_CATEGORY_01 = 3,
        SMALL_OPAC_CATEGORY_10 = 4,
        SMALL_OPAC_CATEGORY_11 = 5,
        SMALL_OPAC_CATEGORY_12 = 6,
        SMALL_OPAC_CATEGORY_21 = 7,
        SMALL_OPAC_CATEGORY_22 = 8,
        SMALL_OPAC_CATEGORY_23 = 9,
        SMALL_OPAC_CATEGORY_32 = 10,
        SMALL_OPAC_CATEGORY_33 = 11,
        SMALL_OPAC_CATEGORY_3plus = 12
    }
    enum LARGE_OPAC_STAGE
    {
        LARGE_OPAC_STAGE_O = 1,
       LARGE_OPAC_STAGE_A = 2,
       LARGE_OPAC_STAGE_B = 3,
       LARGE_OPAC_STAGE_C = 4
    }
    enum Q3A
    {
        Q3A_Y = 1,
        Q3A_N = 0,
        Q3A_null = 2
    }
    enum PLEURAL_SITE_PROFILE
    {
        PLEURAL_SITE_PROFILE_O = 1,
        PLEURAL_SITE_PROFILE_R = 2,
        PLEURAL_SITE_PROFILE_L = 4,
        PLEURAL_SITE_PROFILE_RL = 6
    }
    enum PLEURAL_SITE_FACE
    {
        PLEURAL_SITE_FACE_O = 1,
        PLEURAL_SITE_FACE_R = 2,
        PLEURAL_SITE_FACE_L = 4,
        PLEURAL_SITE_FACE_RL = 6
    }
    enum PLEURAL_SITE_DIAPHRAGM
    {
        PLEURAL_SITE_DIAPHRAGM_O = 1,
        PLEURAL_SITE_DIAPHRAGM_R = 2,
        PLEURAL_SITE_DIAPHRAGM_L = 4,
        PLEURAL_SITE_DIAPHRAGM_RL = 6
    }
    enum PLEURAL_SITE_Other
    {
        PLEURAL_SITE_Other_O = 1,
        PLEURAL_SITE_Other_R = 2,
        PLEURAL_SITE_Other_L = 4,
        PLEURAL_SITE_Other_RL = 6
    }
    enum PLEURAL_CALC_PROFILE
    {
        PLEURAL_CALC_PROFILE_O = 1,
        PLEURAL_CALC_PROFILE_R = 2,
        PLEURAL_CALC_PROFILE_L = 4,
        PLEURAL_CALC_PROFILE_RL = 6
    }
    enum PLEURAL_CALC_FACE
    {
        PLEURAL_CALC_FACE_O = 1,
        PLEURAL_CALC_FACE_R = 2,
        PLEURAL_CALC_FACE_L = 4,
        PLEURAL_CALC_FACE_RL = 6
    }
    enum PLEURAL_CALC_DIAPHRAGM
    {
        PLEURAL_CALC_DIAPHRAGM_O = 1,
        PLEURAL_CALC_DIAPHRAGM_R = 2,
        PLEURAL_CALC_DIAPHRAGM_L = 4,
        PLEURAL_CALC_DIAPHRAGM_RL = 6
    }
    [Flags()]
    enum PLEURAL_CALC_Other
    {
        PLEURAL_CALC_Other_O = 1,
        PLEURAL_CALC_Other_R = 2,
        PLEURAL_CALC_Other_L = 4,
        PLEURAL_CALC_Other_RL = 6
    }
    enum PLEURAL_EXTENT_R
    {
        PLEURAL_EXTENT_R_O = 1,
        PLEURAL_EXTENT_R_R = 2
    }
    enum PLEURAL_EXTENT_R_123
    {
        PLEURAL_EXTENT_R_123_1 = 1,
        PLEURAL_EXTENT_R_123_2 = 2,
        PLEURAL_EXTENT_R_123_3 = 4
    }
    enum PLEURAL_EXTENT_L
    {
        PLEURAL_EXTENT_L_O = 1,
        PLEURAL_EXTENT_L_L = 2
    }
    enum PLEURAL_EXTENT_L_123
    {
        PLEURAL_EXTENT_L_123_1 = 1,
        PLEURAL_EXTENT_L_123_2 = 2,
        PLEURAL_EXTENT_L_123_3 = 4
    }
    enum PLEURAL_WIDTH_R
    {
        PLEURAL_WIDTH_R_O = 1,
        PLEURAL_WIDTH_R_R = 2
    }
    enum PLEURAL_WIDTH_R_ABC
    {
        PLEURAL_WIDTH_R_ABC_A = 1,
        PLEURAL_WIDTH_R_ABC_B = 2,
        PLEURAL_WIDTH_R_ABC_C = 4
    }
    enum PLEURAL_WIDTH_L
    {
        PLEURAL_WIDTH_L_O = 1,
        PLEURAL_WIDTH_L_L = 2
    }
    enum PLEURAL_WIDTH_L_ABC
    {
        PLEURAL_WIDTH_L_ABC_A = 1,
        PLEURAL_WIDTH_L_ABC_B = 2,
        PLEURAL_WIDTH_L_ABC_C = 4
    }
    enum Q3C
    {
        Q3C_N = 1,
        Q3C_R = 2,
        Q3C_L = 4,
        Q3C_RL = 6
    }
    enum DIFFUSE_SITE_PROFILE
    {
        DIFFUSE_SITE_PROFILE_O = 1,
        DIFFUSE_SITE_PROFILE_R = 2,
        DIFFUSE_SITE_PROFILE_L = 4,
        DIFFUSE_SITE_PROFILE_RL = 6
    }
    enum DIFFUSE_SITE_FACE
    {
        DIFFUSE_SITE_FACE_O = 1,
        DIFFUSE_SITE_FACE_R = 2,
        DIFFUSE_SITE_FACE_L = 4,
        DIFFUSE_SITE_FACE_RL = 6
    }
    enum DIFFUSE_CALC_PROFILE
    {
        DIFFUSE_CALC_PROFILE_O = 1,
        DIFFUSE_CALC_PROFILE_R = 2,
        DIFFUSE_CALC_PROFILE_L = 4,
        DIFFUSE_CALC_PROFILE_RL = 6
    }
    enum DIFFUSE_CALC_FACE
    {
        DIFFUSE_CALC_FACE_O = 1,
        DIFFUSE_CALC_FACE_R = 2,
        DIFFUSE_CALC_FACE_L = 4,
        DIFFUSE_CALC_FACE_RL = 6
    }
    enum DIFFUSE_EXTENT_R
    {
        DIFFUSE_EXTENT_R_O = 1,
        DIFFUSE_EXTENT_R_R = 2
    }
    enum DIFFUSE_EXTENT_R_123
    {
        DIFFUSE_EXTENT_R_123_1 = 1,
        DIFFUSE_EXTENT_R_123_2 = 2,
        DIFFUSE_EXTENT_R_123_3 = 3
    }
    enum DIFFUSE_EXTENT_L
    {
        DIFFUSE_EXTENT_L_O = 1,
        DIFFUSE_EXTENT_L_L = 2
    }
    enum DIFFUSE_EXTENT_L_123
    {
        DIFFUSE_EXTENT_L_123_1 = 1,
        DIFFUSE_EXTENT_L_123_2 = 2,
        DIFFUSE_EXTENT_L_123_3 = 3
    }
    enum DIFFUSE_WIDTH_R
    {
        DIFFUSE_WIDTH_R_O = 1,
        DIFFUSE_WIDTH_R_R = 2
    }

    enum DIFFUSE_WIDTH_R_ABC
    {
        DIFFUSE_WIDTH_R_ABC_A = 1,
        DIFFUSE_WIDTH_R_ABC_B = 2,
        DIFFUSE_WIDTH_R_ABC_C = 3
    }
    
    enum DIFFUSE_WIDTH_L
    {
        DIFFUSE_WIDTH_L_O = 1,
        DIFFUSE_WIDTH_L_L = 2
    }
    enum DIFFUSE_WIDTH_L_ABC
    {
        DIFFUSE_WIDTH_L_ABC_A = 1,
        DIFFUSE_WIDTH_L_ABC_B = 2,
        DIFFUSE_WIDTH_L_ABC_C = 3
    }
    enum Q4A
    {
        Q4A_N = 0,
        Q4A_Y = 1,
        Q4A_null = 2
    }
    enum OTHER_SYMBOLS
    {
        OTHER_SYMBOLS_AA = 1,
        OTHER_SYMBOLS_AT = 2,
        OTHER_SYMBOLS_AX = 4,
        OTHER_SYMBOLS_BU = 8,
        OTHER_SYMBOLS_CA = 16,
        OTHER_SYMBOLS_CG = 32,
        OTHER_SYMBOLS_CN = 64,
        OTHER_SYMBOLS_CO = 128,
        OTHER_SYMBOLS_CP = 256,
        OTHER_SYMBOLS_CV = 512,
        OTHER_SYMBOLS_DI = 1024,
        OTHER_SYMBOLS_EF = 2048,
        OTHER_SYMBOLS_EM = 4096,
        OTHER_SYMBOLS_ES = 8192,
        OTHER_SYMBOLS_FR = 16384,
        OTHER_SYMBOLS_HI = 32768,
        OTHER_SYMBOLS_HO = 65536,
        OTHER_SYMBOLS_ID = 131072,
        OTHER_SYMBOLS_IH = 262144,
        OTHER_SYMBOLS_KL = 524288,
        OTHER_SYMBOLS_ME = 1048576,
        OTHER_SYMBOLS_PA = 2097152,
        OTHER_SYMBOLS_PB = 4194304,
        OTHER_SYMBOLS_PI = 8388608,
        OTHER_SYMBOLS_PX = 16777216,
        OTHER_SYMBOLS_RA = 33554432,
        OTHER_SYMBOLS_RP = 67108864,
        OTHER_SYMBOLS_TB = 134217728
    }


    enum OtherAbnormalities
    {
        OTHER_DISEASES_EVENTRATION = 1,
        OTHER_DISEASES_HIATAL_HERNIA = 2,
        OTHER_DISEASES_BRONCHOVASCULAR = 4,
        OTHER_DISEASES_HYPERINFLATION = 8,
        OTHER_DISEASES_BONY_CHEST_CAGE = 16,
        OTHER_DISEASES_FRACTURE_HEALED = 32,
        OTHER_DISEASES_FRACTURE_NONHEALED = 64,
        OTHER_DISEASES_SCOLIOSIS = 128,
        OTHER_DISEASES_VERTEBRAL_COLUMN = 256,
        OTHER_DISEASES_AZYGOS_LOBE = 512,
        OTHER_DISEASES_LUNG_DENSITY = 1024,
        OTHER_DISEASES_INFILTRATE = 2048,
        OTHER_DISEASES_NODULAR_LESION = 4096,
        OTHER_DISEASES_FOREIGN_BODY = 8192,
        OTHER_DISEASES_POST_SURGICAL = 16384,
        OTHER_DISEASES_CYST = 32768,
        OTHER_DISEASES_AORTA_ANOMALY = 65536,
        OTHER_DISEASES_VASCULAR = 131072
    }

    enum PhysicianNotificationStatus 
    {
        SEE_DR_Y = 262144,
        SEE_DR_N = 524288,
        SEE_DR_Null = 1
    }


}
