using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

using Shouldly;

using System;

using Xunit;

namespace MySpot.Tests.Unit.Entities;
public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2020-02-02")]
    [InlineData("2025-02-02")]
    [InlineData("2020-02-24")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        var now = DateTime.Parse("2022-02-25");
        var invalidDate = DateTime.Parse(dateString);

        //ARRANGE
        var reservation = new Reservation(Guid.NewGuid(), "John Doe", "ABC 123", new Date(invalidDate));

        //ACT
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

        //ASSERT
        //Assert.NotNull(exception);
        //Assert.IsType<InvalidReservationDateException>(exception);
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        //ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "John Doe", "ABC 123", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        //ACT
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

        //ASSERT
        //Assert.NotNull(exception);
        //Assert.IsType<PakingSpotAlreadyReservedException>(exception);
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PakingSpotAlreadyReservedException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_succeed()
    {
        //ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "John Doe", "ABC 123", reservationDate);

        //ACT
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        //ASSERT
        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        _weeklyParkingSpot.Reservations.ShouldContain(reservation);
    }

    #region ARRANGE

    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _now;
    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTime.Parse("2022-02-25"));
        _weeklyParkingSpot = new WeeklyParkingSpot
            (Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}
