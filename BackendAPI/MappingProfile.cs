using AutoMapper;
using DataAccess.Models;
using DTOs;

namespace BackendAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ClickInfoDTO, ClickInfo>();
            CreateMap<ClickInfo, ClickInfoDTO>();

            CreateMap<LogInfoDTO, LogInfo>();
            CreateMap<LogInfo, LogInfoDTO>();

        }
    }
}
