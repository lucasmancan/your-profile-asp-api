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

    public Nullable<int> id { get; set; }
    public string phone { get; set; }

        [JsonIgnore]
        [ForeignKey("user_id")]
    public virtual User user{ get; set; }
    public string ddd { get; set; }
    public string ddi { get; set; }
        [Column("created_at")]
        public DateTimeOffset createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset updatedAt { get; set; }
    }
}
