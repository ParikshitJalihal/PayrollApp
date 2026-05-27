using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>> filter =null,
            string? includeProperties = null);
        T Get(Expression<Func<T, bool>> expression, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity); 
        void Update(T entity);

        Task<IEnumerable<T>> GetAllsAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T account);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
