using ApiProject.web.DTOs;
using ApiProject.web.Models;
using AutoMapper;

namespace ApiProject.web
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClientInfo, GetClientDTO>();
            CreateMap<AddClientDTO, ClientInfo>();
            //CreateMap<UpdateClientDTO, ClientInfo>();
            


        }
    }
}
