using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Tbl_Country")]
    public class Country : BaseEntity
    {
        [Column("Country_Name")]
        public string CountryName { get; set; }

        [Column("Country_Name_Thai")]
        public string CountryNameThai { get; set; }

    }
}
