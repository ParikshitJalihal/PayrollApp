using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _unitOfWork.Account.AddAsync(account);
            await _unitOfWork.Account.SaveAsync();
        }

        public void DeleteAccount(int id)
        {
            Account account = _unitOfWork.Account.GetByIdAsync(id).Result;
            _unitOfWork.Account.Delete(account);
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _unitOfWork.Account.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _unitOfWork.Account.GetAllsAsync();
        }

        public void UpdateAccount(Account account)
        {
            _unitOfWork.Account.Update(account);
            _unitOfWork.Account.SaveAsync();
        }
    }
}
