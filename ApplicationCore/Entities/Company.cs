using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Company")]
    public class Company : BaseEntity
    {
        [Column("Industry_ID")]
        public int IndustryId { get; set; }

        [Column("Company_Name")]
        public string CompanyName { get; set; }

        [Column("Company_Name_TH")]
        public string CompanyNameTh { get; set; }

        [Column("Company_Card")]
        public string CompanyCard { get; set; }

        [Column("Address_No")]
        public string AddressNo { get; set; }

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
        public bool IndustrialEstate { get; set; }
        public string IndustrialEstInfo { get; set; }
        public string MainProduct { get; set; }
        public string OtherProduct { get; set; }
        public decimal ThaiPercentage { get; set; }
        public decimal ForeignPercentage { get; set; }
        public string EmployeeNo { get; set; }
        public bool ValueChainRawMat { get; set; }
        public bool ValueChainComponent { get; set; }
        public bool ValueChainFinishgood { get; set; }
        public bool ValueChainServices { get; set; }
    }
}
