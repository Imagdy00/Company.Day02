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

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable < T >) _context.Employees.Include(E => E.Department).ToList();
            }
            return _context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (_context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T );
            }
            return _context.Set<T>().Find(id);
        }
        public int Add(T Model)
        {
            _context.Set<T>().Add(Model);
            return _context.SaveChanges();
        }

        public int Update(T Model)
        {
            _context.Set<T>().Update(Model);
            return _context.SaveChanges();
        }

        public int Delete(T Model)
        {
            _context.Set<T>().Remove(Model);
            return _context.SaveChanges();
        }

        

       

       
    }
}
