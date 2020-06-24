using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Industry_Review")]
    public class IndustryReview : BaseEntity
    {
        public int Year { get; set; }

        [Column("Industry_ID")]
        public int IndustryId { get; set; }

        [Column("Country_ID")]
        public int CountryId { get; set; }

        [Column("Html_Information")]
        public string HtmlInformation { get; set; }

    }
}
