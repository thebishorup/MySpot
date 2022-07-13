using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Abstractions;
using MySpot.Application.Command;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotsForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotsForCleaningHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;

    public ReservationsController(ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotsForVehicleHandler, 
        ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotsForCleaningHandler, 
        ICommandHandler<DeleteReservation> deleteReservationHandler)
    {
        _reserveParkingSpotsForVehicleHandler = reserveParkingSpotsForVehicleHandler;
        _reserveParkingSpotsForCleaningHandler = reserveParkingSpotsForCleaningHandler;
        _deleteReservationHandler = deleteReservationHandler;
    }

    [HttpPost("{parkingSpotId:Guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
    {
        await _reserveParkingSpotsForVehicleHandler.HandleAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            ParkingSpotId = parkingSpotId,
            UserId = Guid.Parse(User.Identity.Name)
        });
        return NoContent();
    }

    [HttpPost("reservations/cleaning")]
    public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotsForCleaningHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("reservations/{reservationId:guid}")]
    public async Task<ActionResult> Delete(Guid reservationId)
    {
        await _deleteReservationHandler.HandleAsync(new DeleteReservation(reservationId));
        return NoContent();
    }
}
