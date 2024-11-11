using App.Domain.Core.Contracts.ApplicationService;
using App.Domain.Core.DtoModels.UserDtoModels;
using App.EndPoints.MVC.Serkan.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace App.EndPoints.MVC.Serkan.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var UserList =await _userApplicationService.GetAll(cancellationToken);
            var users = UserList.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                BirthDate = u.BirthDate,
                Gender = u.Gender,
                Province = u.Province,
                City = u.City
            }).ToList();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //    return PartialView("_Create", new UserViewModel());

            //return View(new UserViewModel());
            return PartialView("_Create", new UserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", model);

            var dto = new UserDto
            {
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Province = model.Province,
                City = model.City
            };

            await _userApplicationService.Create(dto, cancellationToken);
            return Json(new { success = true });


        }
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            await _userApplicationService.Delete(Id, cancellationToken);
            return Json(new { success = true });
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id, CancellationToken cancellationToken)
        {
            var Record = await _userApplicationService.GetById(Id, cancellationToken);
            var User = new UserViewModel { 
                Name = Record.Name,
                LastName = Record.LastName,
                BirthDate = Record.BirthDate,
                Gender = Record.Gender,
                Province = Record.Province,
                City = Record.City
            };


            return PartialView("_Update", User);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var User = new UserDto
                {
                    Id=model.Id,
                    Name = model.Name,
                    LastName = model.LastName,
                    BirthDate=model.BirthDate,
                    Gender=model.Gender,
                    Province=model.Province,
                    City=model.City
                };
                await _userApplicationService.Update(User , cancellationToken);
                
            }
            return Json(new { success = true });
            //return RedirectToAction("Index");
        }
    }
}
