using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Abstractions;
using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("parking-spots")]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly ICommandHandler<ReserveParkingSpotForVehicle> _vehicleCommandHandler;
        private readonly ICommandHandler<ReserveParkingSpotForCleaning> _cleaningCommandHandler;
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _queryHandler;

        public ParkingSpotsController(ICommandHandler<ReserveParkingSpotForVehicle> vehicleCommandHandler, 
            ICommandHandler<ReserveParkingSpotForCleaning> cleaningCommandHandler, IQueryHandler<GetWeeklyParkingSpots, 
            IEnumerable<WeeklyParkingSpotDto>> queryHandler)
        {
            _vehicleCommandHandler = vehicleCommandHandler;
            _cleaningCommandHandler = cleaningCommandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
            => Ok(await _queryHandler.HandleAsync(query));

        [HttpPost("{parkingSpotId:Guid}/reservations/vehicle")]
        public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
        {
            await _vehicleCommandHandler.HandleAsync(command with 
            { 
                ReservationId = Guid.NewGuid(),
                ParkingSpotId = parkingSpotId
            });
            return NoContent();
        }

        [HttpPost("reservations/vehicle")]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _cleaningCommandHandler.HandleAsync(command);
            return NoContent();
        }
    }
}
