using Newtonsoft.Json;
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


        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        [JsonPropertyAttribute(PropertyName = "id")]
        public Nullable<int> Id { get; set; }

        [JsonPropertyAttribute(PropertyName = "name")]
        [Column("name")]
        public string Name { get; set; }
        [JsonPropertyAttribute(PropertyName = "countryId")]

        [ForeignKey("country_id"), Column(("country_id"))]
        public int? CountryId { get; set; }
        [JsonPropertyAttribute(PropertyName = "country")]

        public virtual Country Country { get; set; }
        [JsonPropertyAttribute(PropertyName = "createdAt")]

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonPropertyAttribute(PropertyName = "updatedAt")]

        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
