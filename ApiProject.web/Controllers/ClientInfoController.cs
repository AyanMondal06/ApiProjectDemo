using ApiProject.web.DTOs;
using ApiProject.web.Insfrastructure;
using ApiProject.web.Models;
using ApiProject.web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace ApiProject.web.Controllers
{
    
    [ApiController]
    [Route("api/Controller")]
    public class ClientInfoController : ControllerBase
    {
        private readonly IClientServices _IClientService;
        public ClientInfoController(IClientServices iClient)
        {
            _IClientService = iClient;
        }

        [Authorize(Roles= "Company,Admin")]
        [HttpGet("GetAll")]
        public ActionResult<List<GetClientDTO>> Getall()
        {
            try
            {
                return Ok(_IClientService.FetchClientDataList());
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }

        }
        [Authorize(Roles = "Company,Admin")]
        [HttpGet("GetSpecific")]
        public ActionResult<GetClientDTO> Get(string UsernameID)
        {
            try
            {
                return Ok(_IClientService.FetchClientData(UsernameID));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add Client")]
        public async Task<string> Post(AddClientDTO _newClientInfo)
        {
            try
            {
                await _IClientService.AddClientData(_newClientInfo);
                return ("Added");
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit Client")]
        public async Task<string> Put(UpdateClientDTO _updateClient)
        {
            return await _IClientService.UpdateClient(_updateClient);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete Client")]
        public async Task<string> Delete(string UsernameID)
        {
            return await _IClientService.RemoveClientData(UsernameID);
            
        }
    }
}
