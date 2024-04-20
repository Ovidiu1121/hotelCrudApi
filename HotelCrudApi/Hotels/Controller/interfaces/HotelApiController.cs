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
        public abstract Task<ActionResult<IEnumerable<Hotel>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Hotel>> CreateHotel([FromBody] CreateHotelRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Hotel>> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Hotel>> DeleteHotel([FromRoute] int id);

        [HttpGet("{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Hotel))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Hotel>> GetByNameRoute([FromRoute] string name);

    }
}
