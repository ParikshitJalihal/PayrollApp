using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Interfaces
{
    public interface IAccountService
    {
            Task<IEnumerable<Account>> GetAccountsAsync();
            Task<Account> GetAccountByIdAsync(int id);
            Task AddAccountAsync(Account account);
            void UpdateAccount(Account account);
            void DeleteAccount(int id);
    }
}
