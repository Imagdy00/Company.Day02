using Company.Day02.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.Day02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentrepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentrepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
          
            var departments = _departmentrepository.GetAll();
            return View(departments);
        }
    }
}
