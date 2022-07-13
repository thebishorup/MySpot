using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("parking-spots")]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _queryHandler;

        public ParkingSpotsController(IQueryHandler<GetWeeklyParkingSpots, 
            IEnumerable<WeeklyParkingSpotDto>> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
            => Ok(await _queryHandler.HandleAsync(query));
    }
}
