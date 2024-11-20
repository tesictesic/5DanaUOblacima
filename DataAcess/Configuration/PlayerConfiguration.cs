using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    internal class PlayerConfiguration:EntityConfiguration<Player>
    {
        public override void Configure(EntityTypeBuilder<Player> builder)
        {
            base.Configure(builder);
            builder.Property(y => y.nickname).IsRequired().HasMaxLength(150);

            builder.HasIndex(y => y.nickname).IsUnique();

           
        }
    }
}
