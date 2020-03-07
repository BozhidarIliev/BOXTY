namespace Boxty.Data.Configurations
{
    using Boxty.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class BoxtyUserConfiguration : IEntityTypeConfiguration<BoxtyUser>
    {
        public void Configure(EntityTypeBuilder<BoxtyUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
        }
    }
}
