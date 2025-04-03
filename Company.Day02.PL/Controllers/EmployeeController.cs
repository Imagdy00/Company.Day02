using AutoMapper;
using Company.Day02.BLL.Interfaces;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;
using Company.Day02.PL.Healpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Company.Day02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;

        public EmployeeController (
            //IEmployeeRepository employeeRepository , 
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }


            
            //Dictionary : Storage of view
            //1- ViewData : transfer extra information from controller (Action) to view

            //ViewData["message"] = "hello from ViewData";


            //2- ViewBag : transfer extra information from controller (Action) to view

            //ViewBag.Message = "Hello from ViewBag";

            //1 , 2  are inherited from controller class

            return View(employees);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments  = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                if(model.Image is not null)
                {
                   model.ImageName =  DocumentSettings.UploadFile(model.Image, "images");
                }




                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                //_unitOfWork.EmployeeRepository.Update(employee);
                //_unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"employee: with id {id} is not found" });
            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(viewName, employee);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id , string viewName = "Edit")
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"department: with id {id} is not found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName , dto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model , string viewName = "Edit")
        {


            if (ModelState.IsValid)
            {

                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName , "images");
                }

                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }

                //if (id != model.Id) return BadRequest();

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewName , model);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Name,
        //            CreateAt = model.CreateAt
        //        };
        //        var count = _departmentrepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentrepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"department: with id {id} is not found" });
            return await Edit(id, "Delete");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {


            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    if(model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
