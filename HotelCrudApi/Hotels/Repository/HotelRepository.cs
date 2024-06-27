using AutoMapper;
using HotelCrudApi.Data;
using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;
using HotelCrudApi.Hotels.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelCrudApi.Hotels.Repository
{
    public class HotelRepository:IHotelRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HotelRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HotelDto> GetByStarsAsync(int stars)
        {
            var hotel = await _context.Hotels.Where(h => h.Stars == stars).FirstOrDefaultAsync();
            
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> CreateHotel(CreateHotelRequest request)
        {
            var hotel = _mapper.Map<Hotel>(request);

            _context.Hotels.Add(hotel);

            await _context.SaveChangesAsync();

            return _mapper.Map<HotelDto>(hotel);

        }

        public async Task<HotelDto> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            _context.Hotels.Remove(hotel);

            await _context.SaveChangesAsync();

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<ListHotelDto> GetAllAsync()
        {
            List<Hotel> result = await _context.Hotels.ToListAsync();
            
            ListHotelDto listHotelDto = new ListHotelDto()
            {
                hotelList = _mapper.Map<List<HotelDto>>(result)
            };

            return listHotelDto;
        }

        public async Task<HotelDto> GetByIdAsync(int id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> GetByNameAsync(string name)
        {
            var hotel = await _context.Hotels.Where(h => h.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> GetByLocationAsync(string location)
        {
            var hotel = await _context.Hotels.Where(h => h.Location.Equals(location)).FirstOrDefaultAsync();
            
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> UpdateHotel(int id, UpdateHotelRequest request)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            hotel.Name = request.Name??hotel.Name;
            hotel.Location = request.Location??hotel.Location;
            hotel.Stars=request.Stars??hotel.Stars;

            _context.Hotels.Update(hotel);

            await _context.SaveChangesAsync();

            return _mapper.Map<HotelDto>(hotel);

        }
    }
}
