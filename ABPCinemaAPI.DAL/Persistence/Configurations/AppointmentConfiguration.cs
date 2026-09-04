using ABPCinemaAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartTime);

            builder.Property(x => x.EndTime);

            builder.Property(x => x.TotalPrice);

            builder.HasOne(x => x.Hall)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.HallId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Services)
                .WithMany(x => x.Appointments);
        }
    }
}
