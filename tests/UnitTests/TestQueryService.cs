using System.Threading.Tasks;
using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Repository.interfaces;
using HotelCrudApi.Hotels.Service;
using HotelCrudApi.Hotels.Service.interfaces;
using HotelCrudApi.System.Constant;
using HotelCrudApi.System.Exceptions;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestQueryService
{
    Mock<IHotelRepository> _mock;
    IHotelQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IHotelRepository>();
        _service=new HotelQueryService(_mock.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {
        _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListHotelDto());

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllHotels());

        Assert.Equal(exception.Message, Constants.NO_HOTEL_EXIST);       

    }
    
    [Fact]
    public async Task GetAll_ReturnAllHotels()
    {

        var hotels = TestHotelFactory.CreateHotels(5);

        _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(hotels);

        var result = await _service.GetAllHotels();

        Assert.NotNull(result);
        Assert.Contains(hotels.hotelList[1], result.hotelList);

    }
    
    [Fact]
    public async Task GetById_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetById_ReturnHotel()
    {

        var hotel = TestHotelFactory.CreateHotel(5);

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(hotel);

        var result = await _service.GetById(5);

        Assert.NotNull(result);
        Assert.Equal(hotel, result);

    }
    
    [Fact]
    public async Task GetByName_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByName_ReturnHotel()
    {

        var hotel = TestHotelFactory.CreateHotel(5);
        hotel.Name="test";

        _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(hotel);

        var result = await _service.GetByName("test");

        Assert.NotNull(result);
        Assert.Equal(hotel, result);

    }
    
    [Fact]
    public async Task GetByLocation_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByLocationAsync("")).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByLocation(""));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByLocation_ReturnHotel()
    {

        var hotel = TestHotelFactory.CreateHotel(5);
        hotel.Name="test";

        _mock.Setup(repo => repo.GetByLocationAsync("test")).ReturnsAsync(hotel);

        var result = await _service.GetByLocation("test");

        Assert.NotNull(result);
        Assert.Equal(hotel, result);

    }
    
    [Fact]
    public async Task GetByStars_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByStarsAsync(4)).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetByStars(4));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByStars_ReturnHotel()
    {

        var hotel = TestHotelFactory.CreateHotel(5);

        _mock.Setup(repo => repo.GetByStarsAsync(5)).ReturnsAsync(hotel);

        var result = await _service.GetByStars(5);

        Assert.NotNull(result);
        Assert.Equal(hotel, result);

    }
    
    
}