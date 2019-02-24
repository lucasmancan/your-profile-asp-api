using aspApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aspApi.Models
{
    [Table("users")]
    public class User
    {


        public User()
        {
            phones = new HashSet<Phone>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 1)]
        public Nullable<int> id { get; set; }
        [Column("first_name")]
        public string firstName { get; set; }
        [Column("last_name")]
        public string lastName { get; set; }
        [Column("email")]
        public string email { get; set; }
        [Column("bio")]
        public string bio { get; set; }
        [Column("cover_image")]
        public string coverImage { get; set; }
        [Column("profile_image")]
        public string profileImage { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("age")]

        public Nullable<int> age { get; set; }
        [Column("active")]

        public bool active { get; set; }

        [Column("gender")]
      
        public Gender? gender { get; set; }

        [ForeignKey("address_id"), Column("address_id")]
        public int? addressId { get; set; }
        public virtual Address address { get; set; }
        [Column("birth_date")]
        public DateTimeOffset? BirthDate { get; set; }
        [Column("logged_at")]
        public DateTimeOffset? loggedAt { get; set; }
        [Column("created_at")]
        public DateTimeOffset? createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset? updatedAt { get; set; }
        public virtual ICollection<Phone> phones { get; set; }

       
    }

    public enum Gender
    {
         M ,
        F,
         O 
    };
}
