using HCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Interfaces
{
    public interface IEmployeeService
    {

        void UpSertEmployee(EmployeeVM employeeVM);
        List<EmployeeVM> ListEmployee();
    }
}
