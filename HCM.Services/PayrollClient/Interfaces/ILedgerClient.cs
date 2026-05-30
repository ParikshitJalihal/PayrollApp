using HCM.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface ILedgerClient
    {
        Task<IEnumerable<LedgerEntry>> GetAllAsync();
        Task AddAsync(LedgerEntry entry);
    }
}