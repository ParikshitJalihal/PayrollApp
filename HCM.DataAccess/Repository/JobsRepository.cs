using HCM.DataAccess.Data;
using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public class JobsRepository : Repository<Jobs>, IJobsRepository
    {
        private ApplicationDbContext _db;
        public JobsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
