using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    internal class MatchesConfiguration:EntityConfiguration<Matches>
    {
        public override void Configure(EntityTypeBuilder<Matches> builder)
        {
            base.Configure(builder);
            builder.Property(y => y.duration).IsRequired().HasMaxLength(100);
            builder.Property(y => y.winingTeamId).HasMaxLength(100);

            builder.HasOne(y=>y.Team).WithMany(y=>y.Matches).HasForeignKey(y=>y.team1Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(y=>y.Team).WithMany(y=>y.Matches).HasForeignKey(y=>y.team2Id).OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
