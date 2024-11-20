using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    internal class PlayerStatisticConfiguration:EntityConfiguration<PlayerStatistic>
    {
        public override void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            base.Configure(builder);
            builder.Property(y => y.wins).IsRequired().HasMaxLength(150);
            builder.Property(y => y.losses).IsRequired().HasMaxLength(150);
            builder.Property(y => y.hoursPlayed).IsRequired().HasMaxLength(150);
            builder.Property(y => y.elo).IsRequired().HasMaxLength(150);

            builder.HasOne(y => y.Player).WithMany(y => y.PlayerStatistic).HasForeignKey(y => y.player_id).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            
        }
    }
}
