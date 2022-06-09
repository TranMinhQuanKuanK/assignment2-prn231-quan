using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BookAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorOrder { get; set; }
        public int RoyaltyPercentage { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Book Book { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Author Author { get; set; }


    }
}
