﻿using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class VehicleReservation : Reservation
    {
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }

        public VehicleReservation(ReservationId id, EmployeeName employeeName, 
            LicensePlate licensePlate, Capacity capacity, Date date) 
            : base(id, date, capacity)
        {
            EmployeeName = employeeName;
            LicensePlate = licensePlate;
        }

        public void ChangeLicensePlate(LicensePlate licensePlate)
        => LicensePlate = licensePlate;
    }
}