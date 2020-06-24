using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Assessment_Data")]
    public class AssessmentData: BaseEntity
    {
        [Column("Assessment_ID")]
        public int AssessmentId { get; set; }

        [Column("Ass_No")]
        public int AssNo { get; set; }

        public int Score { get; set; }

        [NotMapped]
        public int UserId { get; set; }
    }
}
