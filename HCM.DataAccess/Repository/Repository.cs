using HCM.DataAccess.Data;
using HCM.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
            _db.Candidates.Include(c => c.Jobs).Include(c => c.JobId);
            _db.Employees.Include(e => e.Jobs).Include(e => e.JobId);
            _db.Employees.Include(e => e.Candidate).Include(e => e.CandidateId);
            _db.EmployeePayment.Include(ep => ep.Employee).Include(ep => ep.EmployeeId).
                Include(ep => ep.PayComponent).Include(ep => ep.PayComponentId);
            _db.RequisiteDetails.Include(r => r.Requesites).Include(r => r.ReqId);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T account)
        {
            await _dbSet.AddAsync(account);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> expression, string? includeProperties = null)
        {
            IQueryable<T> queryable = _dbSet;
            queryable = queryable.Where(expression);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(property);
                }
            }
            return queryable.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {

            IQueryable<T> queryable = _dbSet;
            if (filter != null)
                queryable = queryable.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(property);
                }
            }
            return queryable;
        }

        public async Task<IEnumerable<T>> GetAllsAsync()
        {
            IQueryable<T> queryable = _dbSet;
            return await queryable.ToListAsync();

        }

        public Task<T> GetByIdAsync(int id)
        {
            IQueryable<T> queryable = _dbSet;
            return queryable.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }


    }
}
