using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public interface IPayrollResultsRepository : IRepository<PayrollResult>
    {
        Task<IEnumerable<PayrollResult>> GetByPeriodAsync(DateTime start, DateTime end);
    }
}
