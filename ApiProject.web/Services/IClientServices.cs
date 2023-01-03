using ApiProject.web.DTOs;
using ApiProject.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.web.Services
{
    public interface IClientServices
    {
        Task<ServiceResponse<List<GetClientDTO>>> FetchClientDataList();
        Task<ServiceResponse<GetClientDTO>> FetchClientData(string UsernameID);
        Task AddClientData(AddClientDTO _newClientInfo);

        Task<string> UpdateClient(UpdateClientDTO UsernameID);
        Task<String> RemoveClientData(string UsernameID);
    }
}
