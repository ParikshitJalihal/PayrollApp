using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Interfaces
{
    public interface IComponentService
    {
        List<SelectListItem> GetComponentMapToList();
        List<PayComponent> ListPayComponent();
        void UpSertPayComponent(PayComponent payComponent);
    }
}
