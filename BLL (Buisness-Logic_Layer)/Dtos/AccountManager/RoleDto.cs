using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.AccountManager
{
    public class RoleDto
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; }
    }
}
