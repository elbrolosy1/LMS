using BLL.Setting;
using BLL__Buisness_Logic_Layer_.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.BookDto
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Cover image is required")]
        [Display(Name = "Cover Image")]
        [AllowedExtentionValidation(FileSetting.AllowedImages)]
        [Size(FileSetting.MaxImageSizeByte)]
        public IFormFile? Cover { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
