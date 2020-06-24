using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Assessment_Base")]
    public class AssessmentBase: BaseEntity
    {
        public int Year { get; set; }

        [Column("Industry_ID")]
        public int IndustryId { get; set; }

        [Column("Ass_No")]
        public int AssNo { get; set; }

        [Column("Th_Score")]
        public decimal ThScore { get; set; }
    }
}
