using App.Domain.Core.Contracts.ApplicationService;
using App.Domain.Core.DtoModels.UserDtoModels;
using App.EndPoints.RazorPages.Serkan.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoints.RazorPages.Serkan.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserApplicationService _userApplicationService;

        [BindProperty]
        public UserViewModel User { get; set; }

        public CreateModel(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var Record = new UserDto
            {
                Id = User.Id,
                Name = User.Name,
                LastName = User.LastName,
                BirthDate = User.BirthDate,
                Gender = User.Gender,
                Province = User.Province,
                City = User.City
            };
            await _userApplicationService.Create(Record, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
