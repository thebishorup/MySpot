﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Configurations
{
    internal sealed class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
    {
        public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));
            builder.Property(x => x.Week)
                .IsRequired()
                .HasConversion(x => x.From.Value, x => new Week(x));
            builder.Property(x => x.Name)
                .IsRequired()
                .HasConversion(x => x.Value, x => new ParkingSpotName(x));
        }
    }
}
