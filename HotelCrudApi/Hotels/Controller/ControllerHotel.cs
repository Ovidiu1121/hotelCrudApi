using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Controller.interfaces;
using HotelCrudApi.Hotels.Model;
using HotelCrudApi.Hotels.Repository.interfaces;
using HotelCrudApi.Hotels.Service.interfaces;
using HotelCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelCrudApi.Hotels.Controller
{
    public class ControllerHotel: HotelApiController
    {
        private IHotelCommandService _hotelCommandService;
        private IHotelQueryService _hotelQueryService;

        public ControllerHotel(IHotelCommandService hotelCommandService, IHotelQueryService hotelQueryService)
        {
            _hotelCommandService = hotelCommandService;
            _hotelQueryService = hotelQueryService;
        }

        public override async Task<ActionResult<HotelDto>> CreateHotel([FromBody] CreateHotelRequest request)
        {
            try
            {
                var hotel = await _hotelCommandService.CreateHotel(request);

                return Created("Hotelul a fost adaugat",hotel);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<HotelDto>> DeleteHotel([FromRoute] int id)
        {
            try
            {
                var hotel = await _hotelCommandService.DeleteHotel(id);

                return Accepted("", hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListHotelDto>> GetAll()
        {
            try
            {
                var hotel = await _hotelQueryService.GetAllHotels();
                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<HotelDto>> GetByIdRoute([FromRoute] int id)
        {
            try
            {
                var hotel = await _hotelQueryService.GetById(id);
                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public override async Task<ActionResult<HotelDto>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var hotel = await _hotelQueryService.GetByName(name);
                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async override Task<ActionResult<HotelDto>> GetByLocationRoute(string location)
        {
            try
            {
                var hotel = await _hotelQueryService.GetByLocation(location);
                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async override Task<ActionResult<HotelDto>> GetByStarsRoute(int stars)
        {
            try
            {
                var hotel = await _hotelQueryService.GetByStars(stars);
                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<HotelDto>> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelRequest request)
        {
            try
            {
                var hotel = await _hotelCommandService.UpdateHotel(id, request);

                return Ok(hotel);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
