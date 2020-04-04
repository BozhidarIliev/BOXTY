using Boxty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Data.Configurations
{
    public class TableItemConfiguration : IEntityTypeConfiguration<TableItem>
    {
        public void Configure(EntityTypeBuilder<TableItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
