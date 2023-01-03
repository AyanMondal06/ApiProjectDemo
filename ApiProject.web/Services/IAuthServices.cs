using ApiProject.web.Models;

namespace ApiProject.web.Services
{
    public interface IAuthServices
    {
        Task<ServiceResponse<string>> Login(string CompanyName, string Password);
        Task<ServiceResponse<int>> Register(Company Company, string Password);
    }
}
