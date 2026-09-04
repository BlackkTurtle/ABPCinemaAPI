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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Price);

            builder.HasOne(x => x.Hall)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.HallId);

            builder.HasMany(x => x.Appointments)
                .WithMany(x => x.Services);
        }
    }
}
