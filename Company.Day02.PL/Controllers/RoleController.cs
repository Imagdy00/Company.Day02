using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;
using Company.Day02.PL.Healpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using NuGet.Packaging.Signing;
using System.Security.Principal;

namespace Company.Day02.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;

            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,

                    
                });
            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,


                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(roles);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Name);
                if(role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,

                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);

        }






        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { StatusCode = 404, message = $"Role: with id {id} is not found" });

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name,
            };

            return View(viewName, dto);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id, string viewName = "Edit")
        {

            return await Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model, string viewName = "Edit")
        {


            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations ! ");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Operations ! ");
                
                var roleResult = await _roleManager.FindByNameAsync(model.Name);
                if(roleResult is not  null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                ModelState.AddModelError("", "Invalid Operations");
                
            }
            return View(viewName, model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        } 



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {


            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations ! ");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Operations ! ");
 
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                

                ModelState.AddModelError("", "Invalid Operations");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            if (string.IsNullOrEmpty(roleId)) return BadRequest();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound("No Role Found with this id");

            ViewData["RoleId"] = roleId;
            var usersInRole = new List<UserInRoleDto>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleDto
                {
                    UserId = user.Id,
                    UserName = user.UserName!
                };
                if (await _userManager.IsInRoleAsync(user, role.Name!)) userInRole.IsSelected = true;
                else userInRole.IsSelected = false;
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = roleId });
            }
            ModelState.AddModelError("", "Invalid Operation");
            return View(users);
        }

























        //[HttpGet]
        //public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role is null)
        //        return NotFound();
        //    var usersInRole = new List<UserInRoleDto>();

        //    var users = await _userManager.Users.ToListAsync();

        //    foreach (var user in users)
        //    {
        //        var userInRole = new UserInRoleDto()
        //        {
        //            UserId = user.Id,
        //            UserName = user.UserName,
        //        };
        //        if(await _userManager.IsInRoleAsync(user , role.Name))
        //        {
        //            userInRole.IsSelected = true;
        //        }
        //        else
        //        {
        //            userInRole.IsSelected = false;
        //        }

        //        usersInRole.Add(userInRole);
        //    }

        //    return View(usersInRole);

        //}

        //[HttpPost]
        //public async Task<IActionResult> AddOrRemoveUsers(string roleId , List<UserInRoleDto> users)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role is null)
        //        return NotFound();
        //    ViewData["RoleId"] = roleId;


        //    if (ModelState.IsValid)
        //    {
        //        foreach(var user in users)
        //        {
        //            var appUser = await _userManager.FindByIdAsync(user.UserId);
        //            if(appUser is not null)
        //            {
        //                if (user.IsSelected && ! await  _userManager.IsInRoleAsync(appUser , role.Name))
        //                {
        //                    await _userManager.AddToRoleAsync(appUser , role.Name);
        //                }
        //                else if (! user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
        //                {
        //                    await _userManager.RemoveFromRoleAsync(appUser , role.Name);
        //                }
        //            }

        //        }
        //        return RedirectToAction(nameof(Edit) , new {id = roleId});
        //    }

        //    return View(users);
        //}



    }
}
