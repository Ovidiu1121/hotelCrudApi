namespace HotelCrudApi.Dto;

public class ListHotelDto
{
    public ListHotelDto()
    {
        hotelList = new List<HotelDto>();
    }
    
    public List<HotelDto> hotelList { get; set; }
}