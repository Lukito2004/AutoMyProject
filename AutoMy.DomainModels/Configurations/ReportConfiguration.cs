using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.DomainModels.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Reason).IsRequired();
            builder.Property(o => o.SenderAccountId).IsRequired();
        }
    }
}
