using Company.Day02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Day02.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        List<Employee> GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);

        //int Add(Employee Model);
        //int Update(Employee Model);
        //int Delete(Employee Model);
    }
}
