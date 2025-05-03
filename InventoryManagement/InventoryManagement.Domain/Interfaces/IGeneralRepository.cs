using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces
{
    
      public interface IGeneralRepository<T, TId>
    where T : Entity<TId>
    where TId : IEquatable<TId>
        {
        public IQueryable<T> GetAll(bool track = false);
        Task<T> GetByIdAsync(TId id, string[]? include = null, bool track = false);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    

        

          


        }
    }

