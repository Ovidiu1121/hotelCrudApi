using HotelCrudApi.Dto;
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

        public async Task<ListHotelDto> GetAllHotels()
        {
            ListHotelDto hotels = await _repository.GetAllAsync();

            if (hotels.hotelList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_HOTEL_EXIST);
            }

            return hotels;
        }

        public async Task<HotelDto> GetById(int id)
        {
            HotelDto hotels = await _repository.GetByIdAsync(id);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }

        public async Task<HotelDto> GetByName(string name)
        {
            HotelDto hotels = await _repository.GetByNameAsync(name);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }

        public async Task<HotelDto> GetByLocation(string location)
        {
            HotelDto hotels = await _repository.GetByLocationAsync(location);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }

        public async Task<HotelDto> GetByStars(int stars)
        {
            HotelDto hotels = await _repository.GetByStarsAsync(stars);

            if (hotels == null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            return hotels;
        }
    }
}
