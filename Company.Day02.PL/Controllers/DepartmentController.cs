using AutoMapper;
using Company.Day02.BLL.Interfaces;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;
using Company.Day02.BLL.Interfaces;
using Company.Day02.BLL.Repositories;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Company.Rabeea.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        // Ask CLR To Create Object of DepartmentRepository
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var dept = _mapper.Map<Department>(department);
                await _unitOfWork.DepartmentRepository.AddAsync(dept);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (dept is null)
            {
                return NotFound();
            }
            return View(viewName, dept);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (dept is null)
            {
                return NotFound();
            }
            var department = _mapper.Map<CreateDepartmentDto>(dept);
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDto department)
        {
            if (ModelState.IsValid)
            {
                var dept = _mapper.Map<Department>(department);
                dept.Id = id;
                if (id != dept.Id) return BadRequest();
                _unitOfWork.DepartmentRepository.Update(dept);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0) return RedirectToAction("Index");
            }

            return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (dept is null)
            {
                return NotFound();
            }
            _unitOfWork.DepartmentRepository.Delete(dept);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}



































//using Company.Day02.BLL.Interfaces;
//using Company.Day02.DAL.Models;
//using Company.Day02.PL.Dtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;

//namespace Company.Day02.PL.Controllers
//{
//    [Authorize]
//    public class DepartmentController : Controller
//    {
//        //private readonly IDepartmentRepository _departmentrepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
//        {
//            //_departmentrepository = departmentRepository;
//            _unitOfWork = unitOfWork;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {

//            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
//            return View(departments);
//        }



//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();

//        }


//        [HttpPost]
//        public async Task<IActionResult> Create(CreateDepartmentDto model)
//        {
//            if (ModelState.IsValid)
//            {
//                var department = new Department()
//                {
//                    Code = model.Code,
//                    Name = model.Name,
//                    CreateAt = model.CreateAt
//                };
//                await _unitOfWork.DepartmentRepository.AddAsync(department);
//                var count = await _unitOfWork.CompleteAsync();


//                if (count > 0)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(model);

//        }


//        [HttpGet]
//        public async Task<IActionResult> Details(int? id , string viewName = "Details")
//        {
//            if (id is null) return BadRequest("Invalid Id");
//            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
//            if (department is null) return NotFound(new { StatusCode = 404, message = $"department: with id {id} is not found" });
//            return View(viewName , department);
//        }


//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id is null) return BadRequest("Invalid Id");
//            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
//            if (department is null) return NotFound(new { StatusCode = 404, message = $"department: with id {id} is not found" });
//            var dto = new CreateDepartmentDto()
//            {
//                Name = department.Name,
//                Code = department.Code,
//                CreateAt = department.CreateAt,
//            };
//            return View(dto);
//        }



//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDto model )
//        {


//            if (ModelState.IsValid)
//            {
//                var department = new Department()
//                {
//                    Id = id,
//                    Name = model.Name,
//                    Code = model.Code,
//                    CreateAt = model.CreateAt,
//                };

//                _unitOfWork.DepartmentRepository.Update(department);
//                var count = await _unitOfWork.CompleteAsync();

//                if (count > 0)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(model);
//        }



//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
//        //{


//        //    if (ModelState.IsValid)
//        //    {
//        //        var department = new Department
//        //        {
//        //            Id = id,
//        //            Name = model.Name,
//        //            Code = model.Name,
//        //            CreateAt = model.CreateAt
//        //        };
//        //        var count = _departmentrepository.Update(department);
//        //        if (count > 0)
//        //        {
//        //            return RedirectToAction(nameof(Index));
//        //        }
//        //    }
//        //    return View(model);
//        //}


//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id is null) return BadRequest("Invalid Id");
//            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
//            if (department is null) return NotFound(new { StatusCode = 404, message = $"department: with id {id} is not found" });


//            var dto = new CreateDepartmentDto()
//            {
//                Name = department.Name,
//                Code = department.Code,
//                CreateAt = department.CreateAt
//            };
//            return View(dto);
//        }




//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Delete([FromRoute] int id, CreateDepartmentDto model )
//        {


//            if (ModelState.IsValid)
//            {
//                var department = new Department()
//                {
//                    Id = id,
//                    Name = model.Name,
//                    Code = model.Code,
//                    CreateAt = model.CreateAt,

//                };
//                _unitOfWork.DepartmentRepository.Delete(department);
//                var count = await _unitOfWork.CompleteAsync();

//                if (count > 0)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(model);
//        }


//    }




//}
