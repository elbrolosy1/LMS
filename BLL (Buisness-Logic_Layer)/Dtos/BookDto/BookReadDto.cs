using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.BookDto
{
    public class BookReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public int AuthorId { get; set; } // Added

        public int CategoryId { get; set; } // Added
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        public string Description { get; set; } = string.Empty;

        public string? Cover { get; set; }
    }
}
