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
    public class TimeSheetEntryRepository : Repository<TimeSheetEntry>, ITimeSheetRepository
    {
        private ApplicationDbContext _db;
        public TimeSheetEntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
