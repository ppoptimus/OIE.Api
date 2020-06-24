using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Document")]
    public class Document : BaseEntity
    {
        [Column("Document_Name")]
        public string DocumentName { get; set; }

        [Column("Document_File")]
        public string DocumentFile { get; set; }

        [Column("Image_File")]
        public string ImageFile { get; set; }

        [Column("Parent_ID")]
        public int ParentId { get; set; }

        [Column("Show_Home_Page")]
        public bool ShowHomePage { get; set; }

        [NotMapped]
        public string ParentDocumentName { get; set; }

        [NotMapped]
        public IList<Document> DocumentChild { get; set; }

        [NotMapped]
        public int Level { get; set; }
    }
}
