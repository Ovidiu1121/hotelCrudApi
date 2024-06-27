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

public class TestCommandService
{
    Mock<IHotelRepository> _mock;
    IHotelCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IHotelRepository>();
        _service = new HotelCommandService(_mock.Object);
    }
    
    [Fact]
    public async Task Create_InvalidData()
    {
        var create = new CreateHotelRequest()
        {
            Name="Test",
            Location="test",
            Stars=0
        };

        var hotel = TestHotelFactory.CreateHotel(5);

        _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(hotel);
                
        var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateHotel(create));

        Assert.Equal(Constants.HOTEL_ALREADY_EXIST, exception.Message);
        
    }
    
    [Fact]
    public async Task Create_ReturnHotel()
    {

        var create = new CreateHotelRequest()
        {
            Name="Test",
            Location="test",
            Stars=4
        };

        var hotel = TestHotelFactory.CreateHotel(5);

        hotel.Name=create.Name;
        hotel.Location=create.Location;
        hotel.Stars=create.Stars;

        _mock.Setup(repo => repo.CreateHotel(It.IsAny<CreateHotelRequest>())).ReturnsAsync(hotel);

        var result = await _service.CreateHotel(create);

        Assert.NotNull(result);
        Assert.Equal(result, hotel);
    }
    
    [Fact]
    public async Task Update_ItemDoesNotExist()
    {
        var update = new UpdateHotelRequest()
        {
            Name="Test",
            Location="test",
            Stars=0
        };

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateHotel(1, update));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task Update_InvalidData()
    {
        var update = new UpdateHotelRequest()
        {
            Name="Test",
            Location="test",
            Stars=0
        };

        _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateHotel(5, update));

        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Update_ValidData()
    {
        var update = new UpdateHotelRequest()
        {
            Name="Test",
            Location="test",
            Stars=0
        };

        var hotel = TestHotelFactory.CreateHotel(5);

        hotel.Name=update.Name;
        hotel.Location=update.Location;
        hotel.Stars=update.Stars.Value;

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(hotel);
        _mock.Setup(repoo => repoo.UpdateHotel(It.IsAny<int>(), It.IsAny<UpdateHotelRequest>())).ReturnsAsync(hotel);

        var result = await _service.UpdateHotel(5, update);

        Assert.NotNull(result);
        Assert.Equal(hotel, result);

    }

    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.DeleteHotel(It.IsAny<int>())).ReturnsAsync((HotelDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteHotel(5));

        Assert.Equal(exception.Message, Constants.HOTEL_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var hotel = TestHotelFactory.CreateHotel(44);

        _mock.Setup(repo => repo.GetByIdAsync(44)).ReturnsAsync(hotel);

        var result= await _service.DeleteHotel(44);

        Assert.NotNull(result);
        Assert.Equal(hotel, result);


    }
    
    
    
}