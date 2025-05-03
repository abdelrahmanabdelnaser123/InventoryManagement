using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repository
{
    public class GeneralRepository<T, TId> : IGeneralRepository<T, TId> where T : Entity<TId> where TId : IEquatable<TId>
    {
        private readonly InventoryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GeneralRepository(InventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
   

        public IQueryable<T> GetAll(bool track = false)
        {
            if (track)
            {
                return _dbSet;
            }
            else
            {
                return _dbSet.AsNoTracking();
            }
        }
        public async Task<T> GetByIdAsync(TId id, string[]? include = null, bool track = false)
        {
            var query = track ? _dbSet : _dbSet.AsNoTracking();
            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = query.Include(inc);
                }
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
