﻿using MySpot.Application.DTO;
using MySpot.Core.Entities;

namespace MySpot.Infrastructure.DAL.Queries
{
    public static class Extensions
    {
        public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
            => new()
            {
                Id = entity.Id.Value.ToString(),
                Name = entity.Name,
                Capacity = entity.Capacity,
                From = entity.Week.From.Value.DateTime,
                To = entity.Week.To.Value.DateTime,
                Reservations = entity.Reservations.Select(x => new ReservationDto
                {
                    Id = x.Id,
                    EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : null,
                    Date = x.Date.Value.Date
                })
            };

        public static UserDto AsDto(this User user)
            => new()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName
            };
    }
}
