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
    public class EmployeePayRepository : Repository<EmployeePay>, IEmployeePayRepository

    {
        private  readonly ApplicationDbContext _db;
        public EmployeePayRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
