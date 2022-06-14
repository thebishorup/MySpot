using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MySpot.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Configurations
{
    internal sealed class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
    {
        public void Configure(EntityTypeBuilder<CleaningReservation> builder)
        {
        }
    }
}
