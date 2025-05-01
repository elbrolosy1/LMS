using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Repo.BookRepo
{
    public class BookRepo:GenericRepo<Book>, IGenericRepo<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
