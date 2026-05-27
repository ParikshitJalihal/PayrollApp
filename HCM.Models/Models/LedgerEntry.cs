using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class LedgerEntry
    {
        public int Id { get; set; }

        public DateTime EntryDate { get; set; }

        public string Description { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        [DisplayName("Account Type")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }

    

}
