using ApplicationCore.Dtos;

namespace ApplicationCore.Dtos
{
    public class V_M_Industry
    {
        public string ID { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string Industry_Name { get; set; }
        public string Industry_Name_Thai { get; set; }

    }

    public class V_M_Country
    {
        public string ID { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string Country_Name { get; set; }
        public string Country_Name_Thai { get; set; }
    }

    public class V_M_Company
    {
        public string Company_ID { get; set; }
        public string Industry_Name { get; set; }
        public string Company_Name { get; set; }
        public string Company_Name_TH { get; set; }
        public string Company_Card { get; set; }
        public string Address_No { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Soi { get; set; }
        public string Street { get; set; }
        public string Tumbon { get; set; }
        public string Amphur { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string ContactPosition { get; set; }
        public string ContactNo { get; set; }
        public string ContactEmail { get; set; }
        public string IndustrialEstate { get; set; }
        public string IndustrialEstInfo { get; set; }
        public string MainProduct { get; set; }
        public string OtherProduct { get; set; }
        public string ThaiPercentage { get; set; }
        public string ForeignPercentage { get; set; }
        public string EmployeeNo { get; set; }
        public string ValueChainRawMat { get; set; }
        public string ValueChainComponent { get; set; }
        public string ValueChainFinishgood { get; set; }
        public string ValueChainServices { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }
    public class V_M_Index
    {
        public string Index_ID { get; set; }
        public string Index_Name { get; set; }
        public string Index_Level { get; set; }
        public string Index_Parent_ID { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string Hierarchy_Code { get; set; }
    }
    public class V_M_Indicator
    {
        public string ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Indicator_Level { get; set; }
        public string Indicator_Source { get; set; }
        public string Indicator_Parent_ID { get; set; }
        public string Index_ID { get; set; }
        public string Rescale_Formula { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string Hierarchy_Code { get; set; }
    }

    public class V_M_COMPETITIVENESS_INDEX
    {
        public string Index_Id { get; set; }
        public string Indicator_id { get; set; }
        public string Parent_Id { get; set; }
        public string Name { get; set; }
        public string hierarchy_code { get; set; }
        public string Level { get; set; }
    }

    public class V_M_Dimension_Indicator
    {
        public string ID { get; set; }
        public string Index_Name { get; set; }
        public string Index_Level { get; set; }
        public string Index_Parent_ID { get; set; }
        public string Source { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string hierarchy { get; set; }
    }

    public class V_M_Professional_Weight_4
    {
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        //public string Industry_Name_Thai { get; set; }
        public string DM_Indi_ID { get; set; }
        //public string Index_Name { get; set; }
        public string Weight { get; set; }
        public string DropFlag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string hierarchy { get; set; }
    }

    public class V_M_MasterWeight_4 {
        public string ID { get; set; }
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Weight { get; set; }
        public string Source_Type { get; set; }
        public string DropFlag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_M_Pmi_Indicator
    {
        public string ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Indicator_Name_Thai { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_M_Pmi_Industry
    {
        public string ID { get; set; }
        public string Industry_Name { get; set; }
        public string Industry_Name_Thai { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_D_SURVEY
    {
        public string ID { get; set; }
        public string PrimaryH_ID { get; set; }
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name { get; set; }
        public string Country_ID { get; set; }
        public string Country_Name { get; set; }
        public string Company_ID { get; set; }
        public string Company_Name { get; set; }
        public string Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Score { get; set; }
        public string Remark { get; set; }
        public string ImportFlag { get; set; }
        public string DropFlag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }
    public class V_D_PRIMARY
    {
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name { get; set; }
        public string Country_ID { get; set; }
        public string Country_Name { get; set; }
        public string Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Score { get; set; }
        public string MaxScore { get; set; }
        public string MinScore { get; set; }
        public string SumScore { get; set; }
        public string CntScore { get; set; }
        public string Recale_Score { get; set; }
        public string ImportFlag { get; set; }
        public string RefYear { get; set; }
        public string ndFlag { get; set; }
        public string ndSource { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }

    public class V_D_SECONDARY
    {
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name { get; set; }
        public string Country_ID { get; set; }
        public string Country_Name { get; set; }
        public string Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Score { get; set; }
        public string MaxScore { get; set; }
        public string MinScore { get; set; }
        public string SumScore { get; set; }
        public string CntScore { get; set; }
        public string Recale_Score { get; set; }
        public string ImportFlag { get; set; }
        public string RefYear { get; set; }
        public string ndFlag { get; set; }
        public string ndSource { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }

    public class V_D_ASSESSEMENT
    {
        //public string ID  {get; set;}
        public string Year { get; set; }
        public string Industry_ID  {get; set;}
        public string Industry_Name_Thai  {get; set;}
        public string Company_ID  {get; set;}
        public string Company_Name_TH  {get; set;}
        public string Assessment_ID  {get; set;}
        //public string Assessment_Base_ID  {get; set;}    
        public string Ass_No  {get; set;}
        public string Score  {get; set;}
        public string Create_User  {get; set;}
        public string Create_Date {get; set;}
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_D_SURVEY_RECOMEND
    {
        public string ID { get; set; }
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name_Thai { get; set; }
        public string Company_ID { get; set; }
        public string Company_Name_TH { get; set; }
        public string Rec_People { get; set; }
        public string Rec_Mkt { get; set; }
        public string Rec_Capital { get; set; }
        public string Rec_Tech { get; set; }
        public string Rec_Regulation { get; set; }
        public string Per_Production { get; set; }
        public string Sample_Product { get; set; }
        public string Remain_Capac { get; set; }
        public string Remain_Capac_reason { get; set; }
        public string VC_Telecom { get; set; }
        public string VC_Aero { get; set; }
        public string VC_Agro { get; set; }
        public string VC_AI { get; set; }
        public string VC_Industrail { get; set; }
        public string VC_SSD { get; set; }
        public string VC_Med { get; set; }
        public string VC_BD { get; set; }
        public string VC_SmartHome { get; set; }
        public string VC_Automotive { get; set; }
        public string VC_Other { get; set; }
        public string Prod_Component { get; set; }
        public string Prod_Device { get; set; }
        public string Prod_App { get; set; }
        public string OEM_YearPrev2 { get; set; }
        public string OEM_YearPrev1 { get; set; }
        public string ODM_YearPrev2 { get; set; }
        public string ODM_YearPrev1 { get; set; }
        public string OBM_YearPrev2 { get; set; }
        public string OBM_YearPrev1 { get; set; }
        public string GR_Year { get; set; }
        public string GR_Year_Reason { get; set; }
        public string GR_YearNext1 { get; set; }
        public string GR_YearNext1_Reason { get; set; }
        public string Export_Per_Rev { get; set; }
        public string Import_Flag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }

    public class V_D_SECONDARY_THONLY
    {
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name_Thai { get; set; }
        public string Country_ID { get; set; }
        public string Country_Name_Thai { get; set; }
        public string Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Score { get; set; }
        public string ImportFlag { get; set; }
        public string RefYear { get; set; }
        public string ndSource { get; set; }
        public string THOnlyFlag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }

    }

    public class V_R_INDUSTRY_INDICATOR
    {
        public string YEAR { get; set; }
        public string Version { get; set; }
        public string Use_Flag { get; set; }
        //public string ID { get; set; }
        public string Inds_Indicator_ID { get; set; }
        public string Industry_ID { get; set; }
        public string Indicator_ID { get; set; }
        public string Indi_wg { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
        public string Country_Id { get; set; }
        public string Base_Score { get; set; }
        public string Index_Id { get; set; }

    }
    public class V_R_ASSESSMENT_BASE
    {
        public string ID {get; set;}
        public string Year {get; set;}
        public string Version {get; set;}
        public string Industry_ID {get; set;}
        public string Industry_Name_Thai {get; set;}
        public string Ass_No {get; set;}
        public string Th_Score {get; set;}
    }
    public class V_R_TH_5SW
    {
        //public string ID { get; set; }
        public string year { get; set; }
        public string Version { get; set; }
        public string Country_ID { get; set; }
        public string Country_Name { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name_Thai { get; set; }
        public string Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public string Score { get; set; }
        public string DisplayFlag { get; set; }


    }
    public class V_R_COMPETITIVENESS_INDEX
    {
        public string country_id { get; set; }
        public string Country_Name_Thai { get; set; }
        public string Industry_ID { get; set; }
        public string Industry_Name_Thai { get; set; }
        public string Index_ID { get; set; }
        public string indicator_ID { get; set; }
        public string Name { get; set; }
        public string Indi_wg { get; set; }
        public string Base_Score { get; set; }
        public string Level { get; set; }
        public string Year { get; set; }
        public string Version { get; set; }
        public string Hierarchy_Code { get; set; }

    }

    public class V_D_PMI_PART1
    {
        public string ID { get; set; }
        //public string FormDate { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string FormNo { get; set; }
        public string InformantFName { get; set; }
        public string InformantLName { get; set; }
        public string InformantPosition { get; set; }
        public string InformantCompany { get; set; }
        public string PMI_Industry_ID { get; set; }
        public string PMI_Industry_Name { get; set; }
        public string Product { get; set; }
        public string SaleDomestic { get; set; }
        public string SaleOversea { get; set; }
        public string ExportCountry1 { get; set; }
        public string ExportCountry2 { get; set; }
        public string ExportCountry3 { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IndustrySize { get; set; }
        public string CountWorker { get; set; }
        public string CapitalValue { get; set; }
        public string Comment_CurrentMonth_Positive { get; set; }
        public string Comment_CurrentMonth_Negative { get; set; }
        public string Comment_NextMonth_Positive { get; set; }
        public string Comment_NextMonth_Negative { get; set; }
        public string Evaluation { get; set; }
        public string InterviewerFName { get; set; }
        public string InterviewerLName { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }
    public class V_D_PMI_PART2
    {
        public string ID { get; set; }
        public string PMI_ID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Code_ID { get; set; }
        public string Name { get; set; }
        public string Month_CurrentLast_Change { get; set; }
        public string Month_CurrentLast_Percent { get; set; }
        public string Month_CurrentLast_Remark { get; set; }
        public string Month_CurrentNext_Change { get; set; }
        public string Month_CurrentNext3_Change { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_D_PRIMARY_DATA_4 {
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string Company_ID { get; set; }
        public string DM_Indi_ID { get; set; }
        public string Score1 { get; set; }
        public string Score2 { get; set; }
        public string Score3 { get; set; }
        public string DM_Weight { get; set; }
        public string Remark { get; set; }
        public string ImportFlag { get; set; }
        public string DropFlag { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }
    public class V_R_RESULT_4 {
        public string Version { get; set; }
        public string Year { get; set; }
        public string Industry_ID { get; set; }
        public string DM_Indi_ID { get; set; }
        public string Result { get; set; }
        public string Result_Type { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }

    public class V_R_PMI_RESULT {
        public string Year { get; set; }
        public string Month { get; set; }
        public string PMI_Industry_ID { get; set; }
        public string PMI_Indicator_ID { get; set; }
        public string Score { get; set; }
        public string Result_Type { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
        public string Modify_User { get; set; }
        public string Modify_Date { get; set; }
    }
}