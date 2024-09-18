using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T> Entities();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<int> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftRemove(T entity);
        Task<bool> RemoveAsync(T entity);
        Task SaveChangesAsync();
        Task<int> UpdateAsync(T entity);
    }
}
