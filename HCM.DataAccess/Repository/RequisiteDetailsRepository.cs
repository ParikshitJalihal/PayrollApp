using HCM.DataAccess.Data;
using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public class RequisiteDetailsRepository : Repository<RequisiteDetails>, IRequisiteDetailsRepository
    {
        private ApplicationDbContext _db;
        public RequisiteDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
