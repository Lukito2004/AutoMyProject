using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.DomainModels.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.ImageUrl).IsRequired();
            builder.Property(o => o.Price).IsRequired();
            builder.Property(o => o.Price).HasColumnType("decimal(8,2)");
            builder.Property(o => o.Model).IsRequired();
            builder.Property(o => o.PublishingYear).IsRequired();
            builder.Property(o => o.fuelType).IsRequired();
            builder.Property(o => o.VelocityOfEngine).IsRequired();
            builder.Property(o => o.DistanceDrove).IsRequired();
            builder.Property(o => o.Cilinders).IsRequired();
            builder.Property(o => o.GearBox).IsRequired();
            builder.Property(o => o.DriveWheels).IsRequired();
            builder.Property(o => o.Doors).IsRequired();
            builder.Property(o => o.Wheel).IsRequired();
            builder.Property(o => o.Color).IsRequired();
            builder.Property(o => o.CabinColor).IsRequired();
            builder.Property(o => o.AirbagCount).IsRequired();
            builder.Property(o => o.ABS).IsRequired();
            builder.Property(o => o.ElectronicWindows).IsRequired();
            builder.Property(o => o.AirConditioner).IsRequired();
            builder.Property(o => o.ClimateControl).IsRequired();
            builder.Property(o => o.LeatherSaloon).IsRequired();
            builder.Property(o => o.Disks).IsRequired();
            builder.Property(o => o.Navigation).IsRequired();
            builder.Property(o => o.CentralLocker).IsRequired();
            builder.Property(o => o.Hatch).IsRequired();
            builder.Property(o => o.Signaling).IsRequired();
            builder.Property(o => o.OnBoardComputer).IsRequired();
            builder.Property(o => o.Hydraulics).IsRequired();
            builder.Property(o => o.AntiSkid).IsRequired();
            builder.Property(o => o.ArmchairsHeating).IsRequired();
            builder.Property(o => o.ParkingControl).IsRequired();
            builder.Property(o => o.BackViewCamera).IsRequired();
            builder.Property(o => o.TechBrowsing).IsRequired();
            builder.Property(o => o.Description).IsRequired();
        }
    }
}
