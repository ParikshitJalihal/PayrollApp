using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; } // e.g., Asset, Liability, Expense, Revenue

        [ValidateNever]
        public virtual ICollection<LedgerEntry> LedgerEntries { get; set; }
    }



}
