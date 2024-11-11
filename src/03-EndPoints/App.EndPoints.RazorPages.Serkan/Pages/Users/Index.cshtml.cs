using App.Domain.Core.Contracts.ApplicationService;
using App.EndPoints.RazorPages.Serkan.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoints.RazorPages.Serkan.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserApplicationService _userApplicationService;

        public IndexModel(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        public List<UserViewModel> Users { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var userDtos = await _userApplicationService.GetAll(cancellationToken);

            
            Users = userDtos.Select(dto => new UserViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Province = dto.Province,
                City = dto.City
            }).ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userApplicationService.Delete(id, cancellationToken);
            return RedirectToPage();  // پس از حذف، صفحه را دوباره بارگذاری می‌کنیم.
        }
    }
}
