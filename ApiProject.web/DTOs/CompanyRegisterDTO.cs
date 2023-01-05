using ApiProject.web.Insfrastructure;
using ApiProject.web.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.DTOs
{
    public class CompanyRegisterDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z ]*[A-Za-z]$", ErrorMessage = "Only Alphabets and space allowed")]
        public String CompanyName { get; set; } = String.Empty;


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z ]*[A-Za-z]$", ErrorMessage = "Only Alphabets and space allowed")]
        public String CompanyCEO { get; set; } = String.Empty;

        //public AppRoles Role { get; set; } = AppRoles.Company;

        public string Password { get; set; } = String.Empty;
    }
}
