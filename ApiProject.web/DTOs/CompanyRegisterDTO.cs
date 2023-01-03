using ApiProject.web.Insfrastructure;
using ApiProject.web.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.DTOs
{
    public class CompanyRegisterDTO
    {

        public String CompanyName { get; set; } = String.Empty;

        public String CompanyCEO { get; set; } = String.Empty;

        //public AppRoles Role { get; set; } = AppRoles.Company;

        public string Password { get; set; } = String.Empty;
    }
}
