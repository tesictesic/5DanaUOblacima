using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    internal class TeamConfiguration:EntityConfiguration<Team>
    {
        public override void Configure(EntityTypeBuilder<Team> builder)
        {
            base.Configure(builder);
            builder.Property(y => y.teamName).IsRequired().HasMaxLength(150);

            builder.HasIndex(y => y.teamName).IsUnique();
        }
    }
}
