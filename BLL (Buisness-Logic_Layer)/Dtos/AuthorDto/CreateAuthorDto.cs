using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.AuthorDto
{
    public class CreateAuthorDto
    {
     
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public int BornYear { get; set; }
        
        [Required]
        public int DeathYear { get; set; }
        
        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be Male and Female")]
        public string Gender { get; set; }
    }
}
