using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;
using Newtonsoft.Json;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class HotelIntegrationTests:IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HotelIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/ControllerHotel/create";
        var hotel = new CreateHotelRequest() { Name = "new name", Location = "new location", Stars = 3 };
        var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Hotel>(responseString);

        Assert.NotNull(result);
        Assert.Equal(hotel.Name, result.Name);
        Assert.Equal(hotel.Location, result.Location);
        Assert.Equal(hotel.Stars, result.Stars);

    }
    
    [Fact]
    public async Task Post_Create_HotelAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/ControllerHotel/create";
        var hotel = new CreateHotelRequest() { Name = "new name", Location = "new location", Stars = 3 };
        var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }
    
    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/ControllerHotel/create";
        var hotel = new CreateHotelRequest() { Name = "new name", Location = "new location", Stars = 3 };
        var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Hotel>(responseString)!;

        request = "/api/v1/ControllerHotel/update/"+result.Id;
        var updateHotel = new UpdateHotelRequest() { Name = "updated name", Location = "updated location", Stars = 44 };
        content = new StringContent(JsonConvert.SerializeObject(updateHotel), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Hotel>(responseString)!;

        Assert.Equal(updateHotel.Name, result.Name);
        Assert.Equal(updateHotel.Location, result.Location);
        Assert.Equal(updateHotel.Stars, result.Stars);
    }
    
    [Fact]
    public async Task Put_Update_HotelDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/ControllerHotel/update/1";
        var updateHotel = new UpdateHotelRequest() { Name = "updated name", Location = "updated location", Stars = 44 };
        var content = new StringContent(JsonConvert.SerializeObject(updateHotel), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_HotelExists_ReturnsDeletedHotel()
    {

        var request = "/api/v1/ControllerHotel/create";
        var hotel = new CreateHotelRequest() { Name = "new name", Location = "new location", Stars = 3  };
        var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Hotel>(responseString)!;

        request = "/api/v1/ControllerHotel/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_HotelDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/ControllerHotel/delete/66";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        
    }

    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/ControllerHotel/create";
        var hotel = new CreateHotelRequest() { Name = "new name", Location = "new location", Stars = 3 };
        var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Hotel>(responseString)!;

        request = "/api/v1/ControllerHotel/name/" + result.Name;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetByName_HotelDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/ControllerHotel/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
    
}