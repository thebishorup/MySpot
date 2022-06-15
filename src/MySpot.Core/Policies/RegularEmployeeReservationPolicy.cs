﻿using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies
{
    internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
    {
        private readonly IClock _clock;

        public RegularEmployeeReservationPolicy(IClock clock)
        {
            _clock = clock;
        }

        public bool CanBeApplied(JobTitle jobTitle)
            => jobTitle == JobTitle.Employee;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> parkingSpots, EmployeeName employeeName)
        {
            var totalEmpReservations = parkingSpots
                .SelectMany(x => x.Reservations)
                .OfType<VehicleReservation>()
                .Count(x => x.EmployeeName == employeeName);

            return totalEmpReservations <= 2 && _clock.Current().Hour > 2;
        }
    }
}
