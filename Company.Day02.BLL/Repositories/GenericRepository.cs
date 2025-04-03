using Company.Day02.BLL.Interfaces;
using Company.Day02.DAL.Data.Contexts;
using Company.Day02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Day02.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable < T >) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?>  GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as T );
            }
            return _context.Set<T>().Find(id);
        }
        public async Task AddAsync(T Model)
        {
            await _context.AddAsync(Model);
            
        }

        public void Update(T Model)
        {
            _context.Set<T>().Update(Model);
            
        }

        public void Delete(T Model)
        {
            _context.Set<T>().Remove(Model);
            
        }

        

       

       
    }
}
