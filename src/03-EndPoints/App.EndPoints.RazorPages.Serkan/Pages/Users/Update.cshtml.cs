using App.Domain.Core.Contracts.ApplicationService;
using App.Domain.Core.DtoModels.UserDtoModels;
using App.EndPoints.RazorPages.Serkan.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoints.RazorPages.Serkan.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly IUserApplicationService _userApplicationService;

        [BindProperty]
        public UserViewModel User { get; set; }

        public UpdateModel(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        // متد OnGetAsync برای دریافت داده‌های کاربر
        public async Task OnGetAsync(int Id, CancellationToken cancellationToken)
        {
            var record = await _userApplicationService.GetById(Id, cancellationToken);

            // مقداردهی به مدل User برای ارسال به ویو
            User = new UserViewModel
            {
                Id = record.Id,
                Name = record.Name,
                LastName = record.LastName,
                BirthDate = record.BirthDate,
                Gender = record.Gender,
                Province = record.Province,
                City = record.City
            };
        }

        // متد OnPostAsync برای ارسال داده‌ها به سرور و بروزرسانی
        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var userDto = new UserDto
                {
                    Id = User.Id,
                    Name = User.Name,
                    LastName = User.LastName,
                    BirthDate = User.BirthDate,
                    Gender = User.Gender,
                    Province = User.Province,
                    City = User.City
                };

                // بروزرسانی اطلاعات کاربر
                await _userApplicationService.Update(userDto, cancellationToken);

                return RedirectToPage("Index");
            }

            // در صورت وجود خطا، صفحه دوباره لود می‌شود
            return Page();
        }
    }
}
