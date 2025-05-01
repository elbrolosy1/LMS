using BLL__Buisness_Logic_Layer_.Dtos.IssueRecordDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Services.IssueRecordService
{
    public interface IIssueRecordService
    {
        Task<IEnumerable<ReadIssueRecord>> GetAllAsync();
        Task<ReadIssueRecord?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateIssueRecord dto);
        Task<bool> UpdateAsync(int id, UpdateIssueRecord dto);
        Task<bool> DeleteAsync(int id);


    }
}
