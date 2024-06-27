using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Hotels.Service.interfaces
{
    public interface IHotelCommandService
    {
        Task<HotelDto> CreateHotel(CreateHotelRequest request);
        Task<HotelDto> UpdateHotel(int id, UpdateHotelRequest request);
        Task<HotelDto> DeleteHotel(int id);
    }
}
