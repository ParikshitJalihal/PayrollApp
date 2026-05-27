using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public interface ILedgerRepository : IRepository<LedgerEntry>
    {
        Task<IEnumerable<LedgerEntry>> GetAllEntriesAsync();
        Task<LedgerEntry> GetEntryByIdAsync(int id);
        Task AddEntryAsync(LedgerEntry entry);
        Task SaveChangesAsync();
    }
}
