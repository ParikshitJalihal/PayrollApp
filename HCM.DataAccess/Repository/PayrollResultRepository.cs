using HCM.DataAccess.Data;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository
{
    public class PayrollResultRepository : Repository<PayrollResult>, IPayrollResultsRepository
    {
        public PayrollResultRepository(ApplicationDbContext db) : base(db)
        {
        }

        public Task<IEnumerable<PayrollResult>> GetByPeriodAsync(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
