using AutoMapper;
using LocaMotoApplication.DTOs.Requests;
using LocaMotoApplication.DTOs.Responses;
using LocaMotoDomain.Entities;
namespace LocaMotoApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MotorcycleRequestDto, Motorcycle>();
            CreateMap<Motorcycle, MotorcycleResponseDto>();
        }
    }
}
