using AutoMapper;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Domain.Entities;

namespace LocaMoto.Application.Mapping
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
