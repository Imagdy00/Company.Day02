﻿using Company.Day02.BLL.Interfaces;
using Company.Day02.DAL.Data.Contexts;
using Company.Day02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Day02.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> ,  IDepartmentRepository

    {

        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }

        //private readonly CompanyDbContext _context;

        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Department> GetAll()
        //{

        //    return _context.Departments.ToList();
        //}


        //public Department? Get(int id)
        //{

        //    return _context.Departments.Find(id);
        //}


        //public int Add(Department Model)
        //{

        //    _context.Departments.Add(Model);
        //    return _context.SaveChanges();
        //}


        //public int Update(Department Model)
        //{

        //    _context.Departments.Update(Model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department Model)
        //{

        //    _context.Departments.Remove(Model);
        //    return _context.SaveChanges();
        //}






    }
}
