using AutoMapper;
using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateHotelRequest, Hotel>();
            CreateMap<UpdateHotelRequest, Hotel>();
            CreateMap<HotelDto, Hotel>().ReverseMap();
        }

    }
}
