using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Repo.CategoryRepo
{
    public class CategoryRepo:GenericRepo<Category>, IGenericRepo<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
