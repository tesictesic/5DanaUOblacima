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
    internal class TeamPlayerConfiguration : IEntityTypeConfiguration<TeamPlayer>
    {
        public void Configure(EntityTypeBuilder<TeamPlayer> builder)
        {
            builder.HasKey(y => new { y.playerId, y.teamId });

            builder.HasOne(y => y.Team).WithMany(y => y.TeamPlayers).HasForeignKey(y => y.teamId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(y=>y.Player).WithMany(y=>y.TeamPlayer).HasForeignKey(y=>y.playerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
