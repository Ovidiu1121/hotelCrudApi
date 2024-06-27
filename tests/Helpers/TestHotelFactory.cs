using HotelCrudApi.Dto;

namespace tests.Helpers;

public class TestHotelFactory
{
    public static HotelDto CreateHotel(int id)
    {
        return new HotelDto
        {
            Id = id,
            Name="Hotel Mamaia"+id,
            Location="Constanta"+id,
            Stars=5+id
        };
    }

    public static ListHotelDto CreateHotels(int count)
    {
        ListHotelDto doctors=new ListHotelDto();
            
        for(int i = 0; i<count; i++)
        {
            doctors.hotelList.Add(CreateHotel(i));
        }
        return doctors;
    }
}