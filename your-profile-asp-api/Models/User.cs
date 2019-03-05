using aspApi.Utils;
using Newtonsoft.Json;
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
            Phones = new HashSet<Phone>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [JsonPropertyAttribute(PropertyName = "id")]
        [Key, Column("id", Order = 1)]
        public Nullable<int> Id { get; set; }
        [JsonPropertyAttribute(PropertyName = "firstName")]
        [Column("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyAttribute(PropertyName = "lastName")]
        [Column("last_name")]
        public string LastName { get; set; }
        [JsonPropertyAttribute(PropertyName = "email")]
        [Column("email")]
        public string Email { get; set; }
        [JsonPropertyAttribute(PropertyName = "bio")]

        [Column("bio")]
        public string Bio { get; set; }
        [Column("cover_image")]
        [JsonPropertyAttribute(PropertyName = "coverImage")]
        public string CoverImage { get; set; }
        [Column("profile_image")]
        [JsonPropertyAttribute(PropertyName = "profileImage")]

        public string ProfileImage { get; set; }
        [Column("password")]
        [JsonPropertyAttribute(PropertyName = "password")]
        [JsonIgnore]
        public string Password { get; set; }
        [Column("age")]
        [JsonPropertyAttribute(PropertyName = "age")]

        public Nullable<int> Age { get; set; }
        [Column("active")]
        [JsonPropertyAttribute(PropertyName = "active")]

        public bool Active { get; set; }
        [JsonPropertyAttribute(PropertyName = "gender")]

        [Column("gender")]
      
        public Gender? Gender { get; set; }
        [JsonPropertyAttribute(PropertyName = "addressId")]

        [ForeignKey("address_id"), Column("address_id")]
        public int? AddressId { get; set; }
        [JsonPropertyAttribute(PropertyName = "address")]

        public virtual Address Address { get; set; }
        [JsonPropertyAttribute(PropertyName = "birthDate")]
        [Column("birth_date")]
        public DateTimeOffset? BirthDate { get; set; }
        [JsonPropertyAttribute(PropertyName = "loggedAt")]

        [Column("logged_at")]
        public DateTimeOffset? LoggedAt { get; set; }
        [JsonPropertyAttribute(PropertyName = "createdAt")]

        [Column("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }
        [Column("updated_at")]
        [JsonPropertyAttribute(PropertyName = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }
        [JsonPropertyAttribute(PropertyName = "phones")]
        public virtual ICollection<Phone> Phones { get; set; }

       
    }

    public enum Gender
    {
         M ,
        F,
         O 
    };
}
