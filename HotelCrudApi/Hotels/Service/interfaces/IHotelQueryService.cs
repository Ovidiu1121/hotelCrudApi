using HotelCrudApi.Hotels.Model;

namespace HotelCrudApi.Hotels.Service.interfaces
{
    public interface IHotelQueryService
    {
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel> GetById(int id);
        Task<Hotel> GetByName(string name);
    }
}
