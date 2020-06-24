using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Assessment")]
    public class Assessment: BaseEntity
    {
        public int Year { get; set; }

        [Column("Industry_ID")]
        public int IndustryId { get; set; }

        [Column("Company_ID")]
        public int CompanyId { get; set; }

        public string Remark { get; set; }

        [NotMapped]
        public int UserId { get; set; }
    }
}
