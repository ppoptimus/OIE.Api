using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_PMI")]
    public class PMIHEAD : BaseEntity
    {

        [Column("FormNo")]
        public string FormNo { get; set; }

        [Column("InformantFName")]
        public string InformantFName { get; set; }

        [Column("InformantLName")]
        public string InformantLName { get; set; }

        [Column("InformantPosition")]
        public string InformantPosition { get; set; }

        [Column("InformantCompany")]
        public string InformantCompany { get; set; }

        [Column("PMI_Industry_ID")]
        public int PMI_Industry_ID { get; set; }

        [Column("Product")]
        public string Product { get; set; }

        [Column("SaleDomestic")]
        public double SaleDomestic { get; set; }

        [Column("SaleOversea")]
        public double SaleOversea { get; set; }

        [Column("ExportCountry1")]
        public string ExportCountry1 { get; set; }

        [Column("ExportCountry2")]
        public string ExportCountry2 { get; set; }

        [Column("ExportCountry3")]
        public string ExportCountry3 { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Fax")]
        public string Fax { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("IndustrySize")]
        public string IndustrySize { get; set; }

        [Column("CountWorker")]
        public string CountWorker { get; set; }

        [Column("CapitalValue")]
        public string CapitalValue { get; set; }

        [Column("Comment_CurrentMonth_Positive")]
        public string Comment_CurrentMonth_Positive { get; set; }

         [Column("Comment_CurrentMonth_Negative")]
        public string Comment_CurrentMonth_Negative { get; set; }

        [Column("Comment_NextMonth_Positive")]
        public string Comment_NextMonth_Positive { get; set; }

        [Column("Comment_NextMonth_Negative")]
        public string Comment_NextMonth_Negative { get; set; }

        [Column("Evaluation")]
        public string Evaluation { get; set; }

        [Column("InterviewerFName")]
        public string InterviewerFName { get; set; }

        [Column("InterviewerLName")]
        public string InterviewerLName { get; set; }

        [Column("Year")]
        public int Year { get; set; }

         [Column("Month")]
        public int Month { get; set; }
    }

   [Table("Tbl_PMI_Data")]
     public class PMIDATA : BaseEntity
    {

        [Column("PMI_ID")]
        public int PMI_ID { get; set; }

        [Column("Code_ID")]
        public int Code_ID { get; set; }
        
        [Column("Month_CurrentLast_Change")]
        public string Month_CurrentLast_Change { get; set; }

        [Column("Month_CurrentLast_Percent")]
        public double Month_CurrentLast_Percent { get; set; }

        [Column("Month_CurrentLast_Remark")]
        public String Month_CurrentLast_Remark { get; set; }

        [Column("Month_CurrentNext_Change")]
        public String Month_CurrentNext_Change { get; set; }

        [Column("Month_CurrentNext3_Change")]
        public String Month_CurrentNext3_Change { get; set; }
    }

    [Table("Tbl_PMI_Indicator")]
    public class PMIIndicator 
    {
          /*
              'code_ID': 1,
              'NameList': '1. New Order : เปรียบเทียบจำนวนคำสั่งซื้อทั้งหมด <br>(ในประเทศและต่างประเทศ)',
              'month_CurrentLast_Change': '',
              'month_CurrentLast_Percent': 0,
              'month_CurrentLast_Remark': '',
              'month_CurrentNext_Change': '',
              'month_CurrentNext3_Change': '',
          */
        [Column("Code_ID")] // ในฐาน ID
        public int Code_ID { get; set; }

        [Column("NameList")]
        public string NameList { get; set; }

        [Column("Month_CurrentLast_Change")]
        public string Month_CurrentLast_Change { get; set; }

        [Column("Month_CurrentLast_Percent")]
        public double Month_CurrentLast_Percent { get; set; }

        [Column("Month_CurrentLast_Remark")]
        public String Month_CurrentLast_Remark { get; set; }

        [Column("Month_CurrentNext_Change")]
        public String Month_CurrentNext_Change { get; set; }

        [Column("Month_CurrentNext3_Change")]
        public String Month_CurrentNext3_Change { get; set; }
    }

}
