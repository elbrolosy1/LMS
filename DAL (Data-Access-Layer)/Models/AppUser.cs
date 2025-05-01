using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Models
{
    public class AppUser: IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        //public string FullNameUser { get; set; }

        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string gender { get; set; }  

        public ICollection<IssueRecord> IssueRecords { get; set; } = new List<IssueRecord>();
    }
}