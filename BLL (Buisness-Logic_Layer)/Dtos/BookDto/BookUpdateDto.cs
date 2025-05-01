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
        public class BookUpdateDto
        {
            [Required]
            public int Id { get; set; }  // مهم علشان نعرف بنعدّل أنهي كتاب

            [Required]
            public string Title { get; set; }

            [Required]
            public string ISBN { get; set; }

            [Display(Name = "Cover Image")]
            [AllowedExtentionValidation(FileSetting.AllowedImages)]
            [Size(FileSetting.MaxImageSizeByte)]
            public IFormFile? NewCover { get; set; }

            public string? ExistingCoverPath { get; set; }  // نحتفظ بالصورة القديمة لو المستخدم ما رفعش واحدة جديدة

            [Required]
            [Display(Name = "Author")]
            public int AuthorId { get; set; }

            [Required]
            [Display(Name = "Category")]
            public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;


        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        }

}
