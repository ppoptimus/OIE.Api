using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_PrimaryData")]
    public class PrimaryData : BaseEntity
    {
        [Column("PrimaryH_ID")]
        public int PrimaryHId { get; set; }

        public int Year { get; set; }

        [Column("Industry_ID")]
        public int IndustryId { get; set; }

        [Column("Country_ID")]
        public int CountryId { get; set; }

        [Column("Company_ID")]
        public int CompanyId { get; set; }

        [Column("Indicator_ID")]
        public int IndicatorId { get; set; }

        public decimal? Score { get; set; }
        public string Remark { get; set; }
        public string ImportFlag { get; set; }
        public string DropFlag { get; set; }
    }
}
