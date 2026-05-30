using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface IJobsClient
    {
        Task<IEnumerable<Jobs>> GetAllAsync();
        Task<Jobs?> GetByIdAsync(int id);
        Task AddJob(Jobs job);
        Task UpdateAsync(Jobs job);
        Task DeleteAsync(int id);
    }
}
