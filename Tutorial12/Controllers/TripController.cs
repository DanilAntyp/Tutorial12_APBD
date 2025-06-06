using Microsoft.AspNetCore.Mvc;
using Tutorial12.DTO;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

public class TripController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripServise service;

        public TripsController(ITripServise service)
        {
            service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await service.GetTripsAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AddClientDTO dto)
        {
            var result = await service.AssignClientToTripAsync(idTrip, dto);
            if (result == "OK") return Ok();
            return BadRequest(result);
        }
    }
}