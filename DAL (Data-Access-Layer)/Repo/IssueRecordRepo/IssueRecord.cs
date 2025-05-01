using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.Repo.IssueRecordRepo
{
    public class IssueRecordRepo : GenericRepo<IssueRecord>, IGenericRepo<IssueRecord>
    {
        private readonly ApplicationDbContext _context;

        public IssueRecordRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
