using BLL__Buisness_Logic_Layer_.Dtos.BookDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos
{
    public class DashboardViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalBooks { get; set; }
        public int TotalMagazines { get; set; }
        public int TotalNewspapers { get; set; }
        public int TotalIssued { get; set; }
        public int TotalReturned { get; set; }
        public int TotalNotReturned { get; set; }
        public IEnumerable<BookReadDto> RecentBooks { get; set; }
    }
}
