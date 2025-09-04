using AdApp.Application.DTO.Responses;
using AdApp.Domain.Entities;
using AdApp.Domain.ValueObjects;
using AutoMapper;

namespace AdApp.Application.Mappers
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<AdPlatform, AdPlatformResponse>();
            CreateMap<AdPlatformLocation, AdPlatformLocationResponse>();
        }
    }
}
