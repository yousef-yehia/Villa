using AutoMapper;
using Villa_MVC.Models;
using Villa_MVC.Models.Dto;

namespace Villa_MVC.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumberDTO>().ReverseMap();


        }
    }
}
