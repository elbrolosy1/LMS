using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.IssueRecordDto
{
    public class ReadIssueRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public List<string> BookTitles { get; set; } = new List<string>();
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<int> BookIds { get; set; } = new List<int>();
        public string UserName { get; internal set; }
    }
}
