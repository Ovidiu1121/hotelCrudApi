using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelCrudApi.Hotels.Controller.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class HotelApiController: ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Hotel>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListHotelDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> CreateHotel([FromBody] CreateHotelRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> DeleteHotel([FromRoute] int id);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> GetByIdRoute([FromRoute] int id);
        
        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> GetByNameRoute([FromRoute] string name);
        
        [HttpGet("location/{location}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> GetByLocationRoute([FromRoute] string location);

        [HttpGet("stars/{stars}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<HotelDto>> GetByStarsRoute([FromRoute] int stars);
        
    }
}
