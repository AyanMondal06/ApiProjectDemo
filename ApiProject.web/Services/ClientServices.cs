using ApiProject.web.DTOs;
using ApiProject.web.Insfrastructure;
using ApiProject.web.Models;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.web.Services
{
    public class ClinetServices : IClientServices
    {
        private readonly DbInfo _dbInfo;
        private readonly IMapper _mapper;
        public ClinetServices(DbInfo dbInfo,IMapper mapper)
        {
            _dbInfo=dbInfo;
            _mapper=mapper;
        }
 
        public async Task<ServiceResponse<List<GetClientDTO>>> FetchClientDataList()
        {
            var _ServiceResponse = new ServiceResponse<List<GetClientDTO>>();
            var result = (_dbInfo.ClientTable.Select(x => new
            {
                Email = x.Email,
                Username = x.Username,
                Password = x.Password,
            })
             .OrderBy(y => Guid.NewGuid())
             .Take(2)
             ).ToList();
            
            _ServiceResponse.Data = result.Select(c => _mapper.Map<GetClientDTO>(c)).ToList();
             return _ServiceResponse;
 
        }
        public async Task<ServiceResponse<GetClientDTO>> FetchClientData(string UsernameID)
        {
            var _ServiceResponse = new ServiceResponse<GetClientDTO>();
            // List<GetClientDTO> result = _dbInfo.ClientTable.ToList();
            var result = _dbInfo.ClientTable.FirstOrDefault(c => c.Username.Equals(UsernameID));
            _ServiceResponse.Data = _mapper.Map<GetClientDTO>(result);
            return _ServiceResponse;
        }

        public async Task AddClientData(AddClientDTO _newClientInfo)
        {
            var _ServiceResponse = new ServiceResponse<GetClientDTO>();
            _dbInfo.ClientTable.Add(_mapper.Map<ClientInfo>(_newClientInfo));
            await _dbInfo.SaveChangesAsync();
  
        }

        public async Task<string> UpdateClient(UpdateClientDTO User)
        {
            try
            {
                var result = _dbInfo.ClientTable.First(c => c.Username.Equals(User.Username));
                result.Username=User.Username;
                result.Password = User.Password;
                result.Email=User.Email;
                
                

                await _dbInfo.SaveChangesAsync();
                return User.Username + " Modified";
            }
            catch(Exception ex) 
            {
                return ex.Message;
            }
        }

        public async Task<string> RemoveClientData(string UsernameID)
        {
            var _ServiceResponse = new ServiceResponse<GetClientDTO>();
            try
            {
                var result = _dbInfo.ClientTable.FirstOrDefault(c => c.Username.Equals(UsernameID));
                _dbInfo.ClientTable.Remove(_mapper.Map<ClientInfo>(result));
                await _dbInfo.SaveChangesAsync();
                _ServiceResponse.Message = UsernameID + " Removed";
                return _ServiceResponse.Message;
            }
            catch (Exception ex)
            {
                _ServiceResponse.Success = false;
                _ServiceResponse.Message = ex.Message;
                return _ServiceResponse.Message;
            }

            
        }
    }
}
