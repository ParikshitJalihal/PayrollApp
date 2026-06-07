using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJobsRepository Jobs { get; }
        ICandidateRepository Candidate { get; }
        ILedgerRepository Ledger { get; }
        IAccountRepository Account { get; }

        IEmployeeRepository Employee { get; }

        IRequesitesRepository Requesites { get; }
        IRequisiteDetailsRepository RequisiteDetails { get; }
        ITimeSheetRepository TimeSheet { get; }
        ITaskRepository TaskRepo { get; }
        IComponentRepository ComponentRepository { get; }

        IEmployeePayRepository EmployeePayRepository { get; }

        IPayrollResultsRepository PayrollResultsRepository { get; }

        //IInterviewRepository Interview { get; }
        //IEmployeeRepository Employee { get; }
        void Save();
    }
}
