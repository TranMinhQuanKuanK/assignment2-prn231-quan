using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public int PubId { get; set; }
        public int Price { get; set; }
        public string Advance { get; set; }
        public string Royalty { get; set; }
        public int YtdSale { get; set; }
        public string Notes { get; set; }
        public DateTime PublishedDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Publisher Publisher { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
