using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    [Table("phones")]
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyAttribute(PropertyName = "id")]
        [Column("id")]


        public Nullable<int> Id { get; set; }

        [JsonPropertyAttribute(PropertyName = "phone")]
        [Column("phone")]

        public string PhoneNumber { get; set; }

        [JsonIgnore]
        [ForeignKey("user_id")]
        [JsonPropertyAttribute(PropertyName = "user")]
        public virtual User User { get; set; }

        [Column("ddd")]

        [JsonPropertyAttribute(PropertyName = "ddd")]


        public string DDD { get; set; }

        [JsonPropertyAttribute(PropertyName = "ddi")]
        [Column("ddi")]

        public string DDI { get; set; }


        [Column("created_at")]
        [JsonPropertyAttribute(PropertyName = "createdAt")]

        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyAttribute(PropertyName = "updatedAt")]

        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
