using ApiProject.web.Insfrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.Models
{
    public class ClientInfo
    {
        [Key]
        public string Username { get; set; } = "N/A";
        [EmailAddress]
        public string Email { get; set; } =String.Empty;

        [PasswordPropertyText]
        public string Password { get; set; }=string.Empty;
        

        public Company? Company { get; set; }
    }
}
