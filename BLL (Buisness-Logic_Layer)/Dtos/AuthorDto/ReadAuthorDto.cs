using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.AuthorDto
{
    public class ReadAuthorDto
    {
        

            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int BornYear { get; set; }


            public int DeathYear { get; set; }


            public string Gender { get; set; }
        
    }
}
