using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int BornYear { get; set; }
        public int DeathYear { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }


        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
