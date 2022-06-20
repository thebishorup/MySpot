﻿using MySpot.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.DTO
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
    }
}
