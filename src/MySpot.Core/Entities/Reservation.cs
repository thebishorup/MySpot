﻿using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;
public abstract class Reservation
{
    public ReservationId Id { get; }
    public Capacity Capacity { get; }
    public Date Date { get; private set; }

    protected Reservation(ReservationId id, Date date, Capacity capacity)
    {
        Id = id;
        Date = date;
        Capacity = capacity;
    }
}
