using ApiProject.web.Insfrastructure;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z0-10]", ErrorMessage = "Only Alphabets and space allowed")]
        public String CompanyName { get; set; }=String.Empty;


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z ]*[A-Za-z]$", ErrorMessage = "Only Alphabets and space allowed")]
        public String CompanyCEO { get; set; }=String.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public AppRoles Role { get; set; } = AppRoles.Company;
        public List<ClientInfo> ClientList { get; set; }

    }
}
