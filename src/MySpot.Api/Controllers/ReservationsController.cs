using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Application.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<ReservationDto[]>> Get()
    {
        return Ok(await _reservationService.GetAllWeeklyAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> Get(Guid id)
    {
        var reservation = await _reservationService.GetAsync(id);

        if(reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost("vehicle")]
    public async Task<IActionResult> Post(ReserveParkingSpotForVehicle command)
    {
        await _reservationService.ReserveSpotForVehicleAsync(command with { ReservationId = Guid.NewGuid()});
        return CreatedAtAction(nameof(Get), new { Id = command.ReservationId }, default);
    }

    [HttpPost("cleaning")]
    public async Task<IActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reservationService.ReserveParkingForCleaningAsync(command);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ChangeReservationLicensePlate command)
    {
        await _reservationService.ChangeReservedVehicleLicensePlateAsync(command with { ReservationId = id});
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reservationService.DeleteAsync(new DeleteReservation(id));
        return NoContent();
    }
}
