using ApiProject.web.Insfrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiProject.web.DTOs
{
    public class AddClientDTO
    {
        [Key]
        public string Username { get; set; } = "N/A";
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

    }
}
