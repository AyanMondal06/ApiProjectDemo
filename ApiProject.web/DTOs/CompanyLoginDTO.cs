using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.DTOs
{
    public class CompanyLoginDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z ]*[A-Za-z]$", ErrorMessage = "Only Alphabets and space allowed")]
        public string CompanyName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = String.Empty;
    }
}
