using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Hotels.Service.interfaces
{
    public interface IHotelQueryService
    {
        Task<ListHotelDto> GetAllHotels();
        Task<HotelDto> GetById(int id);
        Task<HotelDto> GetByName(string name);
        Task<HotelDto> GetByLocation(string location);
        Task<HotelDto> GetByStars(int stars);
    }
}
