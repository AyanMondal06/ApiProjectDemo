using ApiProject.web.Insfrastructure;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public String CompanyName { get; set; }=String.Empty;

        public String CompanyCEO { get; set; }=String.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public AppRoles Role { get; set; } = AppRoles.Company;
        public List<ClientInfo> ClientList { get; set; }

    }
}
