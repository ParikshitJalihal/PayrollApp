using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface IRequisiteClient
    {
        Task<IEnumerable<RequisiteDetails>> GetAllAsync();
        Task<Requesites?> GetByIdAsync(int id);
        Task AddAsync(RequisiteDetails req);
        Task UpdateAsync(RequisiteDetails req);
        Task<IEnumerable<Requesites>> GetRequesites();
        Task DeleteAsync(int id);
    }
}
