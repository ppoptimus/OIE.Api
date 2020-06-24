using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    // This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    // Using non-generic integer types for simplicity and to ease caching logic
    public class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Create_User")]
        public int CreateUser { get; set; }

        [Column("Create_Date")]
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        [Column("Modify_User")]
        public int ModifyUser { get; set; }

        [JsonIgnore]
        [Column("Modify_Date")]
        public DateTime ModifyDate { get; set; }

        //public int? DeletedBy { get; set; }
        //public DateTime? DeletedAt { get; set; }
        //public bool DeleteFlag { get; set; }
    }
}
