namespace Boxty.Data.Configurations
{
    using Boxty.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BoxtyUserConfiguration : IEntityTypeConfiguration<BoxtyUser>
    {
        public void Configure(EntityTypeBuilder<BoxtyUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Email);
            builder.Property(x => x.UserName);
        }
    }
}
