using Company.Day02.BLL.Interfaces;
using Company.Day02.DAL.Data.Contexts;
using Company.Day02.DAL.Models;
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
            return _context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
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
