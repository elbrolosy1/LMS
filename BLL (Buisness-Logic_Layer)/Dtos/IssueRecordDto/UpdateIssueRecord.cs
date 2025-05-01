using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.IssueRecordDto
{
    public class UpdateIssueRecord
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public List<int> BookIds { get; set; } = new List<int>();

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
    }
}
