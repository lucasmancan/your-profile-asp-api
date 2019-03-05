using Newtonsoft.Json;
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
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        [JsonPropertyAttribute(PropertyName = "id")]

        public Nullable<int> Id { get; set; }
        [JsonPropertyAttribute(PropertyName = "name")]

        public string Name { get; set; }
        [JsonPropertyAttribute(PropertyName = "stateId")]

        [ForeignKey("state_id"), Column("state_id")]
        public int? StateId { get; set; }
        [JsonPropertyAttribute(PropertyName = "state")]

        public virtual State State { get; set; }

        [JsonPropertyAttribute(PropertyName = "createdAt")]

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonPropertyAttribute(PropertyName = "updatedAt")]

        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
    