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

        public async Task<Hotel> CreateHotel(CreateHotelRequest request)
        {
            var hotel = _mapper.Map<Hotel>(request);

            _context.Hotels.Add(hotel);

            await _context.SaveChangesAsync();

            return hotel;

        }

        public async Task<Hotel> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            _context.Hotels.Remove(hotel);

            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Hotel> GetByNameAsync(string name)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task<Hotel> UpdateHotel(int id, UpdateHotelRequest request)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            hotel.Name = request.Name??hotel.Name;
            hotel.Location = request.Location??hotel.Location;
            hotel.Stars=request.Stars??hotel.Stars;

            _context.Hotels.Update(hotel);

            await _context.SaveChangesAsync();

            return hotel;

        }
    }
}
