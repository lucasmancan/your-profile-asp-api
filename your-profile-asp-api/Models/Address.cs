using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    [Table("addresses")]
    public class Address
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        public Nullable<int> id { get; set; }
        public string street { get; set; }
        [Column("zip_code")]
        public string zipCode { get; set; }
        public string neighborhood { get; set; }
        public string number { get; set; }
        [ForeignKey("city_id"), Column("city_id")]
        public int? cityId { get; set; }
        public virtual City city { get; set; }
        [Column("created_at")]
        public DateTimeOffset createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset updatedAt { get; set; }
    }
}
