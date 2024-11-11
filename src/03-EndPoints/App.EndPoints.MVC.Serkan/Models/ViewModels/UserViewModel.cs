using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.Serkan.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name ="نام")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public string Name { get; set; } = null!;
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public string LastName { get; set; } = null!;
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public string Gender { get; set; } = null!;
        [Display(Name = "استان")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public string Province { get; set; } = null!;
        [Display(Name = "شهر")]
        [Required(ErrorMessage = "{0} اجباری است.")]
        public string City { get; set; } = null!;
    }
}
