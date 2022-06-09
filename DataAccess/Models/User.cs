using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public int PubId { get; set; }
        public DateTime HireDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public virtual Role Role { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public virtual Publisher Publisher { get; set; }

    }
}
