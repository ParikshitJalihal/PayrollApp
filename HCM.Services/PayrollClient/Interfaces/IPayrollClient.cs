using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface IPayrollClient
    {
        Task<IEnumerable<PayComponent>> GetAllAsync();
        Task UpsertAsync(PayComponent dto);
    }
}
