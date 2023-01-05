using ApiProject.web.DTOs;
using ApiProject.web.Insfrastructure.Error;
using ApiProject.web.Models;
using ApiProject.web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Method For Register
        /// </summary>
    
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(CompanyRegisterDTO request)
        {
            var response = await _authService.Register
                (
                    new Company 
                    { 
                        CompanyName=request.CompanyName,
                        CompanyCEO=request.CompanyCEO,
                        //Role="Company"
                    },
                    request.Password
                );
            if (!response.Success)
            {
                new Error(response.Message);
                return BadRequest(response);
            }
            return Ok(response);    
        }
        /// <summary>
        /// Method To Login into System.This will add the JWT token at successfull attempt to log In
        /// </summary>

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(CompanyLoginDTO request)
        {
            var response = await _authService.Login (request.CompanyName,request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
