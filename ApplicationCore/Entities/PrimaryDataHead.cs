using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_PrimaryData_Head")]
    public class PrimaryDataHead : BaseEntity
    {
        public int Year { get; set; }
        [Column("Industry_ID")]
        public int IndustryId { get; set; }
        [Column("Company_ID")]
        public int CompanyId { get; set; }
        [Column("Rec_People")]
        public string RecPeople { get; set; }
        [Column("Rec_Mkt")]
        public string RecMkt { get; set; }
        [Column("Rec_Capital")]
        public string RecCapital { get; set; }
        [Column("Rec_Tech")]
        public string RecTech { get; set; }
        [Column("Rec_Regulation")]
        public string RecRegulation { get; set; }
        [Column("Per_Production")]
        public string PerProduction { get; set; }
        [Column("Sample_Product")]
        public string SampleProduct { get; set; }
        [Column("Remain_Capac")]
        public string RemainCapac { get; set; }
        [Column("Remain_Capac_reason")]
        public string RemainCapacreason { get; set; }
        [Column("VC_Telecom")]
        public string VCTelecom { get; set; }
        [Column("VC_Aero")]
        public string VCAero { get; set; }
        [Column("VC_Agro")]
        public string VCAgro { get; set; }
        [Column("VC_AI")]
        public string VCAI { get; set; }
        [Column("VC_Industrail")]
        public string VCIndustrail { get; set; }
        [Column("VC_SSD")]
        public string VCSSD { get; set; }
        [Column("VC_Med")]
        public string VCMed { get; set; }
        [Column("VC_BD")]
        public string VCBD { get; set; }
        [Column("VC_SmartHome")]
        public string VCSmartHome { get; set; }
        [Column("VC_Automotive")]
        public string VCAutomotive { get; set; }
        [Column("VC_Other")]
        public string VCOther { get; set; }
        [Column("Prod_Component")]
        public string ProdComponent { get; set; }
        [Column("Prod_Device")]
        public string ProdDevice { get; set; }
        [Column("Prod_App")]
        public string ProdApp { get; set; }
        [Column("OEM_YearPrev2")]
        public string OEMYearPrev2 { get; set; }
        [Column("OEM_YearPrev1")]
        public string OEMYearPrev1 { get; set; }
        [Column("ODM_YearPrev2")]
        public string ODMYearPrev2 { get; set; }
        [Column("ODM_YearPrev1")]
        public string ODMYearPrev1 { get; set; }
        [Column("OBM_YearPrev2")]
        public string OBMYearPrev2 { get; set; }
        [Column("OBM_YearPrev1")]
        public string OBMYearPrev1 { get; set; }
        [Column("GR_Year")]
        public string GRYear { get; set; }
        [Column("GR_Year_Reason")]
        public string GRYearReason { get; set; }
        [Column("GR_YearNext1")]
        public string GRYearNext1 { get; set; }
        [Column("GR_YearNext1_Reason")]
        public string GRYearNext1Reason { get; set; }
        [Column("Export_Per_Rev")]
        public string ExportPerRev { get; set; }
        [Column("Import_Flag")]
        public string ImportFlag { get; set; }
        [Column("Rec_Industry40")]
        public string RecIndustry40 { get; set; }
    }
}
