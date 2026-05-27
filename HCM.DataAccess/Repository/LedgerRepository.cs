using HCM.DataAccess.Data;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository
{
    public class LedgerRepository :Repository<LedgerEntry> ,ILedgerRepository
    {
        private readonly ApplicationDbContext _context;

        public LedgerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LedgerEntry>> GetAllEntriesAsync()
            => await _context.LedgerEntries.Include(le => le.Account).ToListAsync();

        public async Task<LedgerEntry> GetEntryByIdAsync(int id) => await
                _context.LedgerEntries.Include(le => le.Account)
                                                   .FirstOrDefaultAsync(le => le.Id == id);

        public async Task AddEntryAsync(LedgerEntry entry)
        {
            await _context.LedgerEntries.AddAsync(entry);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
