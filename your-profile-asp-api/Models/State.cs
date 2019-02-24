using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    [Table("states")]
    public class State
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        public Nullable<int> id { get; set; }
        public string name { get; set; }



        [ForeignKey("country_id"), Column(("country_id"))]
        public int? countryId { get; set; }
        public virtual Country country { get; set; }
        [Column("created_at")]
        public DateTimeOffset createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset updatedAt { get; set; }
    }
}
