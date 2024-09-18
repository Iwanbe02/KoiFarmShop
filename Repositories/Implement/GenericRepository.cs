﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly KoiFarmShopContext _context;
        public DbSet<T> _dbSet;
        private readonly ICurrentTime _currentTime;
        

        public GenericRepository(KoiFarmShopContext context, ICurrentTime currentTime)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _currentTime = currentTime;
            
        }
        public async Task<int> AddAsync(T entity)
        {
            entity.CreatedDate = _currentTime.GetCurrentTime();
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public DbSet<T> Entities()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftRemove(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = _currentTime.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = _currentTime.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            entity.ModifiedDate = _currentTime.GetCurrentTime();
            return await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
