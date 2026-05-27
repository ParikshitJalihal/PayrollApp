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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Update(Employee employee)
        //{
        //    var objFromDb = _db.Employees.FirstOrDefault(u => u.EmployeeId == employee.EmployeeId);
        //    if (objFromDb != null)
        //    {
        //        objFromDb.EmployeeName = employee.EmployeeName;
        //        objFromDb.EmployeeDob = employee.EmployeeDob;
        //        objFromDb = employee.Email;
        //        objFromDb.PhoneNumber = employee.PhoneNumber;
        //        objFromDb.DepartmentId = employee.DepartmentId;
        //        objFromDb.JobId = employee.JobId;
        //        _db.SaveChanges();
        //    }
        //}
    }
}
