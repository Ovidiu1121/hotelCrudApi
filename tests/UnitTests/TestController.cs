using System.Threading.Tasks;
using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Controller;
using HotelCrudApi.Hotels.Controller.interfaces;
using HotelCrudApi.Hotels.Service.interfaces;
using HotelCrudApi.System.Constant;
using HotelCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    Mock<IHotelCommandService> _command;
    Mock<IHotelQueryService> _query;
    HotelApiController _controller;

    public TestController()
    {
        _command = new Mock<IHotelCommandService>();
        _query = new Mock<IHotelQueryService>();
        _controller = new ControllerHotel(_command.Object, _query.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {

        _query.Setup(repo => repo.GetAllHotels()).ThrowsAsync(new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST));
           
        var result = await _controller.GetAll();
        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(404, notFound.StatusCode);
        Assert.Equal(Constants.HOTEL_DOES_NOT_EXIST, notFound.Value);

    }

    [Fact]
    public async Task GetAll_ValidData()
    {

        var Hotels = TestHotelFactory.CreateHotels(5);

        _query.Setup(repo => repo.GetAllHotels()).ReturnsAsync(Hotels);

        var result = await _controller.GetAll();

        var okresult = Assert.IsType<OkObjectResult>(result.Result);

        var HotelsAll = Assert.IsType<ListHotelDto>(okresult.Value);

        Assert.Equal(5, HotelsAll.hotelList.Count);
        Assert.Equal(200, okresult.StatusCode);


    }
    
    [Fact]
    public async Task Create_InvalidData()
    {

        var create = new CreateHotelRequest()
        {
            Name = "test",
            Location = "test",
            Stars = 0
        };

        _command.Setup(repo => repo.CreateHotel(It.IsAny<CreateHotelRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.HOTEL_ALREADY_EXIST));

        var result = await _controller.CreateHotel(create);

        var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(400,bad.StatusCode);
        Assert.Equal(Constants.HOTEL_ALREADY_EXIST, bad.Value);

    }

    [Fact]
    public async Task Create_ValidData()
    {

        var create = new CreateHotelRequest()
        {
            Name = "test",
            Location = "test",
            Stars = 4
        };

        var hotel = TestHotelFactory.CreateHotel(5);

        hotel.Name=create.Name;
        hotel.Location=create.Location;
        hotel.Stars=create.Stars;

        _command.Setup(repo => repo.CreateHotel(create)).ReturnsAsync(hotel);

        var result = await _controller.CreateHotel(create);

        var okResult= Assert.IsType<CreatedResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 201);
        Assert.Equal(hotel, okResult.Value);

    }
    
    [Fact]
    public async Task Update_InvalidDate()
    {

        var update = new UpdateHotelRequest()
        {
            Name = "test",
            Location = "test",
            Stars = 4
        };

        _command.Setup(repo => repo.UpdateHotel(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST));

        var result = await _controller.UpdateHotel(1, update);

        var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(bad.StatusCode, 404);
        Assert.Equal(bad.Value, Constants.HOTEL_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Update_ValidData()
    {

        var update = new UpdateHotelRequest()
        {
            Name = "test",
            Location = "test",
            Stars = 4
        };

        var hotel = TestHotelFactory.CreateHotel(5);
        hotel.Name=update.Name;
        hotel.Location=update.Location;
        hotel.Stars=update.Stars.Value;

        _command.Setup(repo=>repo.UpdateHotel(5,update)).ReturnsAsync(hotel);

        var result = await _controller.UpdateHotel(5, update);

        var okResult=Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 200);
        Assert.Equal(okResult.Value, hotel);
    }
    
    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _command.Setup(repo=>repo.DeleteHotel(7)).ThrowsAsync(new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST));

        var result= await _controller.DeleteHotel(7);

        var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(notfound.StatusCode, 404);
        Assert.Equal(notfound.Value, Constants.HOTEL_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var hotel = TestHotelFactory.CreateHotel(5);

        _command.Setup(repo => repo.DeleteHotel(1)).ReturnsAsync(hotel);

        var result = await _controller.DeleteHotel(1);

        var okResult=Assert.IsType<AcceptedResult>(result.Result);

        Assert.Equal(202, okResult.StatusCode);
        Assert.Equal(hotel, okResult.Value);

    }
    
    
    
    
    
    
    
}