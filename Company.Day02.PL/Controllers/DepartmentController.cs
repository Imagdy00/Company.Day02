using Company.Day02.BLL.Repositories;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;
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
        


        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count = _departmentrepository.Add(department);

                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();

        }
    }
}
