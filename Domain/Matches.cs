using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Matches:Entity
    {
        public Guid team1Id { get; set; }
        public Guid team2Id { get; set; }
        public int duration { get; set; }
        public Guid? winingTeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
