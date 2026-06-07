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
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;
        public IJobsRepository Jobs { get; private set; }

        public ICandidateRepository Candidate { get; private set; }
        public ILedgerRepository Ledger { get; private set; }

        public IAccountRepository Account {get; private set; }

        public IEmployeeRepository Employee { get; private set; }

        public IRequesitesRepository Requesites { get; private set; }

        public IRequisiteDetailsRepository RequisiteDetails { get; private set; }

        public ITimeSheetRepository TimeSheet { get; private set; }
        public ITaskRepository TaskRepo { get; set; }
        public IComponentRepository ComponentRepository { get; set; }
        public IEmployeePayRepository EmployeePayRepository { get; private set; }
        public IPayrollResultsRepository PayrollResultsRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Jobs = new JobsRepository(_db);
            Candidate = new CandiateRepository(_db);
            Ledger = new LedgerRepository(_db);
            Account = new AccountRepository(_db);
            Employee = new EmployeeRepository(_db);
            Requesites = new RequesitesRepository(_db);
            RequisiteDetails = new RequisiteDetailsRepository(_db);
            TimeSheet = new TimeSheetEntryRepository(_db);
            TaskRepo = new TaskItemRepository(_db);
            ComponentRepository = new ComponentRepository(_db);
            EmployeePayRepository = new EmployeePayRepository(_db);
            PayrollResultsRepository = new PayrollResultRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
