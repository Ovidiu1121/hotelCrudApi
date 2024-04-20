using HotelCrudApi.Hotels.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Hotel> Hotels { get; set; }

    }
}
