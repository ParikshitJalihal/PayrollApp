using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Interfaces
{
    public interface ILedgerService
    {
        Task<IEnumerable<LedgerEntry>> GetLedgerAsync();
        Task AddLedgerEntryAsync(LedgerEntry entry);
    }
}
