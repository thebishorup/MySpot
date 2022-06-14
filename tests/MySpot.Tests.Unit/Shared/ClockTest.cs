using MySpot.Core.Abstractions;

using System;

namespace MySpot.Tests.Unit.Shared
{
    public class ClockTest : IClock
    {
        public DateTime Current() => new(2022, 02, 25);
    }
}