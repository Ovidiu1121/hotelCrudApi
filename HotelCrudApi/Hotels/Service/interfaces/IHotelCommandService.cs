using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Hotels.Service.interfaces
{
    public interface IHotelCommandService
    {
        Task<Hotel> CreateHotel(CreateHotelRequest request);
        Task<Hotel> UpdateHotel(int id, UpdateHotelRequest request);
        Task<Hotel> DeleteHotel(int id);
    }
}
