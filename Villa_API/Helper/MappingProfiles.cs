using AutoMapper;
using Villa_API.Models;
using Villa_API.Models.Dto;

namespace Villa_API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumber>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumber>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumber>().ReverseMap();




        }
    }
}
