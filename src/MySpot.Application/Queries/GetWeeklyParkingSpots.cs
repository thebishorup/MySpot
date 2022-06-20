using MySpot.Application.Abstractions;
using MySpot.Application.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Queries
{
    public class GetWeeklyParkingSpots : IQuery<IEnumerable<WeeklyParkingSpotDto>>
    {
        public DateTime? Date { get; set; }
    }
}
