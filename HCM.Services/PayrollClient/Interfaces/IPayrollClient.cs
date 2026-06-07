using HCM.Models.Models;
using HCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface IPayrollClient
    {
        Task<IEnumerable<PayComponent>> GetAllAsync();
        Task UpsertAsync(PayComponent dto);

        Task<IEnumerable<EmployeePay>> GetEmployeePayComponents();
        Task<IEnumerable<PayComponentModel>> GetEmployeePaysAsync(int value);
    }
}
