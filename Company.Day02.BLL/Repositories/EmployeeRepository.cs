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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

       
        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }







        //private readonly CompanyDbContext _context;

        //public EmployeeRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}

        //public Employee? Get(int id)
        //{
        //    return _context.Employees.Find(id);
        //}
        //public int Add(Employee Model)
        //{
        //     _context.Employees.Add(Model);  

        //    return _context.SaveChanges();
        //}

        //public int Update(Employee Model)
        //{
        //    _context.Employees.Update(Model);

        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee Model)
        //{
        //    _context.Employees.Remove(Model);

        //    return _context.SaveChanges();
        //}






    }
}
