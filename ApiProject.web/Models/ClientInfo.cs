using ApiProject.web.Insfrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.Models
{
    public class ClientInfo
    {
        [Key]

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z ]*[A-Za-z]$", ErrorMessage = "Only Alphabets and space allowed")]
        public string Username { get; set; } = "N/A";
        [EmailAddress]
        public string Email { get; set; } =String.Empty;

        [PasswordPropertyText]
        public string Password { get; set; }=string.Empty;
        

        public Company? Company { get; set; }
    }
}
