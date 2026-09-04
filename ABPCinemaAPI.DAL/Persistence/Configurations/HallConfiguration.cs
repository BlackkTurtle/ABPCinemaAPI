using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPCinemaAPI.DAL.Entities;

namespace ABPCinemaAPI.DAL.Persistence.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Capacity);

            builder.Property(x => x.Price);

            builder.HasMany(x => x.Services)
                .WithOne(x => x.Hall)
                .HasForeignKey(x => x.HallId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Appointments)
                .WithOne(x => x.Hall)
                .HasForeignKey(x => x.HallId);
        }
    }
}
