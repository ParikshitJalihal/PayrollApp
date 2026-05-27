using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Implementations
{
   
    public class LedgerService : ILedgerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LedgerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LedgerEntry>> GetLedgerAsync()
            => await _unitOfWork.Ledger.GetAllEntriesAsync();

        public async Task AddLedgerEntryAsync(LedgerEntry entry)
        {
            // Example: enforce double-entry rule
            if (entry.Debit == 0 && entry.Credit == 0)
                throw new InvalidOperationException("Entry must have a debit or credit.");

            await _unitOfWork.Ledger.AddEntryAsync(entry);
            await _unitOfWork.Ledger.SaveChangesAsync();
        }
    }

}
