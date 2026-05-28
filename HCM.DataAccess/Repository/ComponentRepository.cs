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
    public class ComponentRepository : Repository<PayComponent>, IComponentRepository
    {
        private ApplicationDbContext _db;
        public ComponentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
