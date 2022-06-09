using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Publisher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PubId { get; set; }
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public virtual ICollection<User> Users { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public virtual ICollection<Book> Books { get; set; }


    }
}
