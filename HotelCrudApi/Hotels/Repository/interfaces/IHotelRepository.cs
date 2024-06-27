using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Hotels.Repository.interfaces
{
    public interface IHotelRepository
    {
        Task<ListHotelDto> GetAllAsync();
        Task<HotelDto> GetByIdAsync(int id);
        Task<HotelDto> GetByNameAsync(string name);
        Task<HotelDto> GetByLocationAsync(string location);
        Task<HotelDto> GetByStarsAsync(int stars);
        Task<HotelDto> CreateHotel(CreateHotelRequest request);
        Task<HotelDto>UpdateHotel(int id,UpdateHotelRequest request);
        Task<HotelDto> DeleteHotel(int id);
    }
}
