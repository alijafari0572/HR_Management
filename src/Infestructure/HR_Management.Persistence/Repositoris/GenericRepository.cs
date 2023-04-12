﻿using HR_Management.Application.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Persistence.Repositoris
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LeaveManagementDbContext _context;

        public GenericRepository(LeaveManagementDbContext context )
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.AddAsync( entity );
            await _context.SaveChangesAsync();
            return (entity);
        }

        public async Task Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            var entity = await GetById(id);
            return entity!=null;
        }
        public async Task<T> GetById(int Id)
        {
            return await _context.Set<T>().FindAsync(Id); 
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}