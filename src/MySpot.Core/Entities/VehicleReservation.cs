using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class VehicleReservation : Reservation
    {
        public UserId UserId { get; private set; }
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }

        public VehicleReservation(ReservationId id, UserId userId, EmployeeName employeeName, 
            LicensePlate licensePlate, Capacity capacity, Date date) 
            : base(id, date, capacity)
        {
            UserId = userId;
            EmployeeName = employeeName;
            LicensePlate = licensePlate;
        }

        public void ChangeLicensePlate(LicensePlate licensePlate)
            => LicensePlate = licensePlate;
    }
}
