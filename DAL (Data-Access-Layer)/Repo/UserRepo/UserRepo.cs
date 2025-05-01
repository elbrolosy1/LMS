using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Repo.UserRepo
{
    public class UserRepo : GenericRepo<AppUser>, IGenericRepo<AppUser>
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
