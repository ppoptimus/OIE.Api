//using ApplicationCore.Entities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ApplicationCore.ViewModels
//{
//    public class PrimaryDataHeadFilterModel
//    {
//        [Column("ID")]
//        public int Id { get; set; }

//        public int Year { get; set; }

//        [Column("Industry_ID")]
//        public int IndustryId { get; set; }

//        [Column("Country_ID")]
//        public int CountryId { get; set; }

//        [Column("Company_ID")]
//        public int CompanyId { get; set; }

//        [Column("Indicator_ID")]
//        public int IndicatorId { get; set; }

//        public decimal Score { get; set; }

//        public string Remark { get; set; }

//        public string ImportFlag { get; set; }

//        public string DropFlag { get; set; }

//        [Column("Create_User")]
//        public int UserId { get; set; }

//    }
//}