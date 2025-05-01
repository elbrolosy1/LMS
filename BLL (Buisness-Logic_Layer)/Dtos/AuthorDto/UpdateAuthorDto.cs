using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.AuthorDto
{
    public class UpdateAuthorDto
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int BornYear { get; set; }

        [Required]
        public int DeathYear { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$")]
        public string Gender { get; set; }
    }
}
