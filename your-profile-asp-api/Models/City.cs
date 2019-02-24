using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    [Table("cities")]
    public class City
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        public Nullable<int> id { get; set; }
    public string name { get; set; }




        [ForeignKey("state_id"), Column("state_id")]
        public int? stateId { get; set; }
        public virtual State state { get; set; }
        [Column("created_at")]
        public DateTimeOffset createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset updatedAt { get; set; }
    }
}
