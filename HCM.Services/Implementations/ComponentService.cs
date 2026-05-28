using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Implementations
{
    public class ComponentService : IComponentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComponentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> GetComponentMapToList()
        {
           List<SelectListItem> mapToList = new List<SelectListItem>()
           {
                new SelectListItem { Value = "1", Text = "Provident Fund" },
                new SelectListItem { Value = "2", Text = "Employer PF" },
                new SelectListItem { Value = "3", Text = "Reimbursement" },
                new SelectListItem { Value = "4", Text = "Other" }
             };
            return mapToList;
        }

        public List<PayComponent> ListPayComponent()
        {
           List<PayComponent> payComponents =  _unitOfWork.ComponentRepository.GetAll().ToList();
            return payComponents;
        }

        public void UpSertPayComponent(PayComponent payComponent)
        {
            if (payComponent.PayComponentId == 0)
            {
                _unitOfWork.ComponentRepository.Add(payComponent);
            }
            else
            {
                _unitOfWork.ComponentRepository.Update(payComponent);
            }
            _unitOfWork.Save();
        }
    }
}
