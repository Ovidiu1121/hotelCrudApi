using HotelCrudApi.Hotels.Model;
using HotelCrudApi.Hotels.Repository.interfaces;
using HotelCrudApi.Hotels.Service.interfaces;
using HotelCrudApi.System.Constant;
using HotelCrudApi.System.Exceptions;

namespace HotelCrudApi.Hotels.Service
{
    public class HotelQueryService: IHotelQueryService
    {
        private IHotelRepository _repository;

        public HotelQueryService(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            IEnumerable<Hotel> hotels = await _repository.GetAllAsync();

            if (hotels.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_HOTEL_EXIST);
            }

            return hotels;
        }

        public async Task<Hotel> GetById(int id)
        {
            Hotel hotels = await _repository.GetByIdAsync(id);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }

        public async Task<Hotel> GetByName(string name)
        {
            Hotel hotels = await _repository.GetByNameAsync(name);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }
    }
}
