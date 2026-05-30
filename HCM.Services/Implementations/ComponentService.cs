using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Interfaces;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Implementations
{
    public class ComponentServiceRemote : IComponentService
    {
        private readonly IPayrollClient _payrollClient;
        public ComponentServiceRemote(IPayrollClient payrollClient)
        {
            _payrollClient = payrollClient;
        }

        // keep the original static mapping (same choices as original)
        public List<SelectListItem> GetComponentMapToList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "Provident Fund" },
                new SelectListItem { Value = "2", Text = "Employer PF" },
                new SelectListItem { Value = "3", Text = "Reimbursement" },
                new SelectListItem { Value = "4", Text = "Other" }
            };
        }

        // original interface is synchronous — block on the async client to remain backward compatible
        public List<PayComponent> ListPayComponent()
        {
            var items = _payrollClient.GetAllAsync().GetAwaiter().GetResult();
            return items?.ToList() ?? new List<PayComponent>();
        }

        public void UpSertPayComponent(PayComponent payComponent)
        {
            _payrollClient.UpsertAsync(payComponent).GetAwaiter().GetResult();
        }
    }
}
