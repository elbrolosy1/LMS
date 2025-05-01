using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Models
{
    public class IssueRecord
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
