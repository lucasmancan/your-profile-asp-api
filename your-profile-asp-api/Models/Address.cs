using Newtonsoft.Json;
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
        [JsonPropertyAttribute(PropertyName = "id")]
        public int Id { get; set; }
        [Column("street")]
        [JsonPropertyAttribute(PropertyName = "street")]
        public string Street { get; set; }
        [Column("zip_code")]
        [JsonPropertyAttribute(PropertyName = "zipCode")]
        public string ZipCode { get; set; }
        [Column("neighborhood")]
        [JsonPropertyAttribute(PropertyName = "neighborhood")]

        public string Neighborhood { get; set; }
        [Column("number")]
        [JsonPropertyAttribute(PropertyName = "number")]

        public string Number { get; set; }
        [JsonPropertyAttribute(PropertyName = "cityId")]

        [ForeignKey("city_id"), Column("city_id")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        [JsonPropertyAttribute(PropertyName = "createdAt")]

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonPropertyAttribute(PropertyName = "updatedAt")]

        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
